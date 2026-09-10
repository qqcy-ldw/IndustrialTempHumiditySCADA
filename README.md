# 工业仓储温湿度监控系统

基于 **WinForms + .NET 8** 开发的仓储温湿度监控小型 SCADA 项目。系统通过 Modbus RTU 采集现场设备数据，完成集中监控、实时趋势、历史趋势、阈值报警、报警记录与用户管理等功能。

> 项目中的设备配置以 Excel 为唯一来源；运行数据、报警记录和用户数据保存到本地 SQLite 数据库，适合小型仓储现场或 Modbus 通信学习场景。

## 功能概览

- 集中监控：展示 A01～A03、B01～B03 六个区域的温度、湿度与通信状态。
- 实时趋势：按秒追加温湿度采样点，可按区域和变量勾选曲线，最多保留最近 10 分钟数据。
- 历史趋势：按时间范围和区域查询 SQLite 历史数据，同时用表格和 ScottPlot 折线图展示。
- Excel 配置：串口参数从 Excel 文件名读取；工作表和变量行描述 Modbus 采集组、地址、数据类型及报警阈值。
- 断线处理：通讯失败时标记分组离线；从站恢复后按重试周期重新连接和读取。
- 报警记录：温度或湿度越过高/低阈值时生成报警，恢复正常后自动更新恢复时间。
- 用户管理：支持管理员和操作员账号、启用/停用、密码修改与账号查询。
- 用户登录：程序启动先验证账号；操作员不能进入“用户管理”页面。
- Modbus 异常响应：RTU 与 TCP 均可解析异常功能码和异常码，并将通信异常返回给调用方。

## 系统界面

主界面提供以下功能入口：

| 页面 | 作用 |
| --- | --- |
| 集中监控 | 展示各仓储分区的实时温湿度与通讯状态 |
| 实时趋势 | 查看当前运行过程中的温湿度变化曲线 |
| 参数配置 | 从 Excel 配置中查看串口参数与温湿度报警阈值 |
| 历史趋势 | 查询 SQLite 中已保存的历史采样记录 |
| 报警记录 | 查询报警发生、恢复、当前值和阈值 |
| 用户管理 | 新增、修改、删除、启停系统用户（管理员权限） |

## 技术栈

| 类别 | 技术 |
| --- | --- |
| 开发平台 | .NET 8、C#、WinForms |
| 通讯协议 | Modbus RTU、Modbus TCP |
| 串口通讯 | `System.IO.Ports` |
| 图表 | ScottPlot WinForms 4.1 |
| 数据库 | SQLite（`Microsoft.Data.Sqlite`） |
| 数据访问 | Dapper |
| Excel 解析 | MiniExcel |

## 项目结构

```text
xbd.WarehouseTHPro
├─ xbd.CommunicationLib       # Modbus RTU/TCP 协议帧、串口与 TCP 通讯基础类
├─ xbd.ControlLib             # 仪表、监控卡片等自定义 WinForms 控件
├─ xbd.NodeSetting            # 设备、分组、变量模型，以及 Excel 配置解析
├─ xbd.WarehouseTHBLL         # 业务逻辑层预留
├─ xbd.WarehouseTHDAL         # SQLite/Dapper 数据访问层
├─ xbd.WarehouseTHModels      # 温湿度、报警、用户等实体模型
├─ xbd.WarehouseTHPro         # WinForms 启动项目、窗体和 Config 配置文件
└─ xbd.WarehouseTHUtils       # 通用工具类
```

## 数据流

```text
Excel 配置
   ↓
ModbusRTUCFG 解析设备、通讯组和变量
   ↓
ModbusRTUDevice 后台轮询 Modbus 从站
   ↓
DeviceBase.CurrentValue 实时缓存
   ├─ 集中监控 / 实时趋势界面
   ├─ 报警阈值判断 → 报警记录
   └─ 每秒批量写入 → SQLite 历史数据
                           ↓
                    历史趋势 / 报警记录 / 用户管理
```

## 设备配置说明

设备配置文件位于启动项目的 [`xbd.WarehouseTHPro/Config`](xbd.WarehouseTHPro/Config) 目录。发布或运行时，该目录会复制到程序输出目录。

### 1. Excel 文件名：串口参数

文件名格式如下：

```text
设备名称_端口号_波特率_校验位_数据位_停止位.xlsx
```

示例：

```text
仓储A区_COM20_9600_None_8_One.xlsx
```

对应参数为：设备名 `仓储A区`、串口 `COM20`、波特率 `9600`、校验位 `None`、数据位 `8`、停止位 `One`。

### 2. 工作表名称：Modbus 通讯组

每个工作表对应一个通讯组，名称格式如下：

```text
分区名称_存储区_从站地址_起始地址_读取长度
```

示例：

```text
A01_保持寄存器_1_0_4
```

系统支持以下存储区名称：

- `保持寄存器` / `HoldingRegister` / `4区`
- `输入寄存器` / `InputRegister` / `3区`
- `线圈` / `Coil` / `0区`
- `离散输入` / `DiscreteInput` / `1区`

工作表中的每一行是一个变量配置。变量名称需要与界面取值规则一致，例如 A01 区温度、湿度可使用 `A01温度`、`A01湿度`。

> 修改 Excel 后请关闭正在打开的表格，再重新启动程序或重新打开“参数配置”页面，使配置重新加载。

## 本地数据库

首次运行时，程序会自动在输出目录创建：

```text
Data/warehouse_th.db
```

数据库包含以下主要表：

| 数据表 | 用途 |
| --- | --- |
| `th_readings` | 每秒保存的温湿度历史采样数据 |
| `th_alarm_records` | 报警发生、恢复、阈值与当前值记录 |
| `sys_users` | 系统用户、角色、启用状态和密码摘要 |

历史数据按时间和区域建立索引；每次采样使用事务批量写入，避免写入过程中产生部分数据。

## 运行方式

### 环境要求

- Visual Studio 2022（建议使用最新稳定版本）
- .NET 8 SDK
- Windows 系统
- 已连接真实 Modbus RTU 从站，或已启动 Modbus 从站模拟器

### 启动步骤

1. 克隆项目后，用 Visual Studio 打开 `xbd.WarehouseTHPro.slnx`。
2. 将 `xbd.WarehouseTHPro` 设置为启动项目。
3. 检查 `xbd.WarehouseTHPro/Config` 下的 Excel 文件名、工作表和变量配置是否与现场设备一致。
4. 确认串口未被其他程序占用，然后按 `F5` 运行。
5. 在登录窗口输入账号密码，登录成功后进入主界面。

首次运行会自动创建默认管理员：

| 账号 | 密码 | 角色 |
| --- | --- | --- |
| `admin` | `123456` | 管理员 |

> 请在实际部署时登录“用户管理”页面修改默认密码，并妥善备份 `Data/warehouse_th.db`。

## 通讯与异常处理

- RTU 轮询线程持续读取配置中的通讯组，将最近一次成功读取的数据写入 `CurrentValue` 缓存。
- 当某一组读取失败时，界面显示异常状态；全部分组失败时设备会进入断线状态，并尝试重新连接。
- Modbus RTU 在校验 CRC 后判断异常响应；Modbus TCP 在统一的 `CheckResponse` 中调用 `ModbusRTU.IsModbusErrorFrame(..., isTcp: true, ...)` 解析异常功能码和异常码。
- 正常数据用于界面展示；集中监控页面每秒最多将一批最新数据写入数据库一次。

## 说明

这是一个面向 WinForms、Modbus 通讯、SQLite 与桌面监控场景的学习型项目。若用于生产环境，建议继续补充：日志系统、数据库备份、密码加盐、角色权限细分、通讯参数在线编辑、历史数据归档与自动化测试。
