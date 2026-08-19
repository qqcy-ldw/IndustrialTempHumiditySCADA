namespace modbus.CommunicationLibl.Lock;

/// <summary>
/// 混合锁：无竞争走用户态(0→1直接进入)，有竞争才切内核态等待。
/// 适用场景：竞争少的短临界区，避免每次进内核的昂贵开销。
/// </summary>
public sealed class SimpleHybridLock : IDisposable
{
    // ===== 核心字段 =====
    // 0=空闲，1=持有且无人等，≥2=有竞争
    private int _waiters;
    // 内核等待句柄，只有竞争时才用
    private readonly AutoResetEvent _waiterLock = new(false);

    /// <summary>
    /// 加锁
    /// </summary>
    public void Enter()
    {
        // 原子+1，谁把_waiters从0变1谁就拿到锁
        if (Interlocked.Increment(ref _waiters) == 1)
            return;                   // 无竞争，拿到锁直接走
        _waiterLock.WaitOne();        // 有竞争，挂起等待
    }

    /// <summary>
    /// 解锁
    /// </summary>
    public void Leave()
    {
        // 原子-1，减到0说明没人排队
        if (Interlocked.Decrement(ref _waiters) == 0)
            return;                   // 无等待者，直接走
        _waiterLock.Set();            // 有等待者，唤醒一个
    }

    // 查询当前是否有线程在排队
    public bool IsWaiting => _waiters != 0;

    // ===== 资源释放 =====
    // 两条释放路径：①手动Dispose → 正常归还   ②忘了调 → GC调终结器兜底

    private bool _disposed;

    /// <summary>手动释放（推荐用using确保调用）</summary>
    public void Dispose()
    {
        ReleaseResources(true);
        GC.SuppressFinalize(this);    // 手动已清，禁止GC再调终结器
    }
    
    /// <summary>终结器：忘了Dispose时GC兜底调用</summary>
    // ~ 是 C# 中定义析构函数（Finalizer）的语法前缀。
    // 析构函数会在垃圾回收器（GC）回收该对象时自动调用，用于清理非托管资源。
    // 注意：不能手动调用，不能带参数，一个类只能有一个析构函数。
    ~SimpleHybridLock()
    {
        ReleaseResources(false);
    }

    /// <summary>
    /// 统一释放入口：true=手动调用，false=GC代劳
    /// </summary>
    private void ReleaseResources(bool disposing)
    {
        if (_disposed) return;
        // disposing=true → 对象还活着，安全释放
        if (disposing) _waiterLock.Dispose();    
        _disposed = true;
    }
}
