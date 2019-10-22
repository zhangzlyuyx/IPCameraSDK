using System;
using System.Collections.Generic;

namespace IPCameraSDK.Common
{
    /// <summary>
    /// 摄像头客户端访问基础类
    /// </summary>
    public abstract class CameraClientBase
    {
        /// <summary>
        /// 获取或设置IP地址
        /// </summary>
        public virtual string IP { get; set; }

        /// <summary>
        /// 获取或设置通讯端口号
        /// </summary>
        public virtual int Port { get; set; }

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 获取或设置通道号(默认0)
        /// </summary>
        public virtual int ChannelId { get; set; }

        /// <summary>
        /// 获取或设置实时预览窗口句柄
        /// </summary>
        public virtual IntPtr RealPlayWnd { get; set; }

        /// <summary>
        /// 获取或设置登录成功是否自动启动实时预览回调
        /// </summary>
        public virtual bool AutoRealDataCallback { get; set; }

        /// <summary>
        /// 获取或设置转流RTMP地址
        /// </summary>
        public virtual string RelayRtmpUrl { get; set; }

        /// <summary>
        /// 获取是否已登录
        /// </summary>
        public virtual bool IsLogin { get; }

        /// <summary>
        /// 获取是否启动实时预览
        /// </summary>
        public virtual bool IsRealPlay { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public abstract KeyValuePair<bool, string> Login();

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public abstract KeyValuePair<bool, string> Logout();

        /// <summary>
        /// 启动实时预览
        /// </summary>
        /// <returns></returns>
        public abstract KeyValuePair<bool, string> StartRealPlay();

        /// <summary>
        /// 停止实时预览
        /// </summary>
        /// <returns></returns>
        public abstract KeyValuePair<bool, string> StopRealPlay();
    }
}