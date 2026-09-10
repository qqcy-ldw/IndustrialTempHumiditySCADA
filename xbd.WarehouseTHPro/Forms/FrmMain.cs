using System.Reflection.Emit;
using System.Globalization;
using xbd.NodeSetting.Common;
using xbd.NodeSetting.ModbusRTU;
using xbd.WarehouseTHDAL;

namespace xbd.WarehouseTHPro
{
    public partial class FrmMain : Form
    {
        /// <summary>固定页：常驻内存，切走只隐藏不销毁</summary>
        private readonly HashSet<string> _fixedPages = new() { "集中监控", "实时趋势" };

        /// <summary>已创建的固定页缓存：标题 → 窗体</summary>
        private readonly Dictionary<string, Form> _pages = new();

        /// <summary>当前显示的普通页（同一时间最多一个）</summary>
        private Form? _currentNormalPage = null;
        private string? _currentNormalTitle = null;
        private readonly AlarmRecordRepository _alarmRepository = new();
        private readonly HashSet<ModbusRTUDevice> _alarmDevices = new();
        private static UserAccount userAccount;

        public FrmMain() : this(null)
        {
        }

        public FrmMain(UserAccount? loginUser)
        {
            InitializeComponent();

            userAccount = loginUser;
            if (loginUser != null)
            {
                lblLoginUser.Text = $"登录用户: {loginUser.UserName}";
            }

            _alarmRepository.Initialize();

            // 左侧菜单所有按钮统一订阅同一个事件
            foreach (Control c in pnlMenu.Controls)
            {
                if (c is Button btn)
                    btn.Click += MenuButton_Click;
            }

            // 固定页启动即创建
            foreach (var title in _fixedPages)
                ShowPage(title);

            // 启动默认显示「集中监控」首页
            ShowPage("集中监控");
        }

        /// <summary>根据标题创建对应的窗体</summary>
        private static Form? CreatePage(string title)
        {
            switch (title)
            {
                case "集中监控": return new FrmCentralMonitor();
                case "实时趋势": return new FrmRealtimeTrend();
                case "参数配置": return new FrmParamConfig();
                case "历史趋势": return new FrmHistoryTrend();
                case "报警记录": return new FrmAlarmRecord();
                case "用户管理":
                if (userAccount.RoleName == "操作员")
                {
                        MessageBox.Show("当前登录用户没有权限访问用户管理页面。", "权限提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    else
                    {
                        return new FrmUserManage();
                    }
                default: return null;
            }
        }

        /// <summary>左侧菜单统一点击事件</summary>
        private void MenuButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;

            ShowPage(btn.Text.Trim());   // 按钮文字去掉前导空格 = 页面标题

            // 高亮当前激活的菜单按钮
            foreach (Control c in pnlMenu.Controls)
            {
                if (c is Button b)
                    b.BackColor = (b == btn)
                        ? Color.FromArgb(56, 59, 138)   // 选中
                        : Color.FromArgb(43, 50, 120);  // 未选中
            }
        }

        /// <summary>切换页面：固定页常驻复用，普通页切走即销毁</summary>
        private void ShowPage(string title)
        {
            if (_fixedPages.Contains(title))
            {
                // ===== 固定页：第一次创建，之后永远复用同一个实例 =====
                if (!_pages.TryGetValue(title, out var form))
                {
                    form = CreatePage(title);
                    if (form is null) return;

                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;

                    _pages[title] = form;
                    pnlContent.Controls.Add(form);
                }
                // 销毁当前普通页（普通页和固定页不共存）
                CloseNormalPage();
                // 只有目标固定页可见
                foreach (var c in _pages)
                    c.Value.Visible = (c.Value == form);

                form.BringToFront();
            }
            else
            {
                // ===== 普通页：每次切换都新建，旧页销毁释放 =====
                if (_currentNormalTitle == title) return;   // 重复点击当前页不重建

                CloseNormalPage();   // 先销毁旧普通页

                var form = CreatePage(title);
                if (form is null) return;

                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                _currentNormalPage = form;
                _currentNormalTitle = title;
                pnlContent.Controls.Add(form);

                // 固定页全部隐藏，只显示普通页
                foreach (var c in _pages)
                    c.Value.Visible = false;

                form.BringToFront();
                // 窗体默认 Visible=false，必须手动显示
                form.Visible = true;
            }
        }

        /// <summary>销毁当前普通页（Remove + Dispose + 清引用，三步缺一不可）</summary>
        private void CloseNormalPage()
        {
            if (_currentNormalPage is null) return;

            pnlContent.Controls.Remove(_currentNormalPage);   // ① 从面板摘下来
            _currentNormalPage.Dispose();                     // ② 释放资源（定时器随之停止）
            _currentNormalPage = null;                        // ③ 清引用
            _currentNormalTitle = null;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] weekDays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            lblSystemTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ") + weekDays[(int)DateTime.Now.DayOfWeek];

            // 集中监控页刷数据
            var monitor = (FrmCentralMonitor)_pages["集中监控"];
            monitor.UpdateMonitor();
            SubscribeAlarmEvents(monitor._devices);

            // 实时趋势页跟着追加一个数据点（用的是同一批设备）
            var trend = (FrmRealtimeTrend)_pages["实时趋势"];
            trend.AppendPoint(monitor._devices);


            ModbusRTUDevice? devA = null;
            ModbusRTUDevice? devB = null;
            foreach (var dev in monitor._devices)
            {
                if (dev.DeviceName.Contains("A区")) devA = dev;
                else if (dev.DeviceName.Contains("B区")) devB = dev;
            }
            if (devA != null)
                lblAreaA.Text = $"A区: {(devA.IsConnected ? "串口已连接" : "串口关闭")} | {devA.CommPeriod} ms";

            if (devB != null)
                lblAreaB.Text = $"B区: {(devB.IsConnected ? "串口已连接" : "串口关闭")} | {devB.CommPeriod} ms";
        }

        /// <summary>
        /// 为每个设备订阅一次报警事件，避免页面刷新时重复订阅。
        /// </summary>
        private void SubscribeAlarmEvents(IEnumerable<ModbusRTUDevice> devices)
        {
            foreach (var device in devices)
            {
                if (_alarmDevices.Add(device))
                {
                    device.AlarmEvent += Device_AlarmEvent;
                    }
                }
            }

        /// <summary>
        /// 报警状态发生变化时写入或关闭数据库记录。
        /// </summary>
        private void Device_AlarmEvent(object sender, AlarmEventArgs e)
        {
            try
            {
                if (e.IsTriggered)
                {
                    _alarmRepository.InsertTriggered(CreateAlarmRecord(e, DateTime.Now));
                }
                else
                {
                    _alarmRepository.MarkRecovered(CreateAlarmRecord(e, DateTime.Now));
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"保存报警记录失败：{ex}");
#endif
            }
        }

        /// <summary>
        ///  根据报警事件参数创建数据库记录对象
        /// </summary>
        /// <param name="alarm"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private static AlarmRecord CreateAlarmRecord(AlarmEventArgs alarm, DateTime time)
        {
            string variableName = alarm.VarName ?? "";
            string zoneName = variableName.Length >= 3 ? variableName[..3] : variableName;
            double? currentValue = ParseNumber(alarm.CurrentValue);
            double? limitValue = ParseNumber(alarm.AlarmValue);
            string valueType = variableName.Contains("湿度") ? "湿度" : "温度";

            return new AlarmRecord
            {
                OccurredAt = time,
                RecoveredAt = alarm.IsTriggered ? null : time,
                DeviceName = alarm.DeviceName ?? "",
                ZoneName = zoneName,
                VariableName = variableName,
                AlarmType = $"{(alarm.IsHighAlarm ? "高" : "低")}{valueType}",
                CurrentValue = currentValue,
                LimitValue = limitValue,
                AlarmNote = alarm.AlarmNote ?? "",
                IsActive = alarm.IsTriggered
            };
        }

        private static double? ParseNumber(string? text)
        {
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out double value)) return value;
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value)) return value;
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using FrmLogin loginForm = new FrmLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                lblLoginUser.Text = $"登录用户: {loginForm.LoginUser.UserName}";
                UserAccountRepository userAccount = new UserAccountRepository();
                var userAccounts = userAccount.Query(loginForm.LoginUser.UserName);
                if (userAccounts.Any() && userAccounts.First().RoleName == "操作员")
                {
                    if (_currentNormalTitle == "用户管理")
                    {
                        CloseNormalPage();
                        MessageBox.Show("当前登录用户没有权限访问用户管理页面。", "权限提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowPage("用户管理");
                }
            }
        }
    }
}
