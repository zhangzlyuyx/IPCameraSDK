using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IPCameraSDK.HaiKang
{
    /// <summary>
    /// 海康摄像头
    /// </summary>
    public class CameraClient : Common.CameraClientBase
    {
        /// <summary>
        /// sdk初始化
        /// </summary>
        private bool sdkInit;

        /// <summary>
        /// 用户Id
        /// </summary>
        private int userId;

        /// <summary>
        /// 用户数据
        /// </summary>
        private IntPtr userPtr;

        /// <summary>
        /// 实时预览句柄
        /// </summary>
        private IntPtr realPlayHandle;

        /// <summary>
        /// 实时预览控件
        /// </summary>
        private object realPlayControl;

        private SDKTypes.FPlayESCallBack realDataCallback;

        /// <summary>
        /// 获取设备信息
        /// </summary>
        public SDKTypes.NET_DVR_DEVICEINFO_V30 DeviceInfo { get; private set; } = new SDKTypes.NET_DVR_DEVICEINFO_V30();

        /// <summary>
        /// 获取预览信息
        /// </summary>
        public SDKTypes.NET_DVR_PREVIEWINFO PreviewInfo { get; private set; } = new SDKTypes.NET_DVR_PREVIEWINFO()
        {
            lChannel = 1,//预览/抓拍的设备通道
            //dwStreamType = 0,//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            dwStreamType = 0,//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            dwLinkMode = 0,//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP
            bBlocked = true,//0- 非阻塞取流，1- 阻塞取流
            dwDisplayBufNum = 15,//播放库播放缓冲区最大缓冲帧数
            byProtoType = 0,//应用层取流协议：0- 私有协议，1- RTSP协议。
            byPreviewMode = 0,
        };

        public override bool IsLogin
        {
            get
            {
                return this.userId > -1;
            }
        }

        public override bool IsRealPlay
        {
            get
            {
                return this.realPlayHandle != IntPtr.Zero;
            }

            set
            {
                base.IsRealPlay = value;
            }
        }

        /// <summary>
        /// rtmp 推流API
        /// </summary>
        private EasyRTMP.EasyRTMPAPI rtmpAPI = new EasyRTMP.EasyRTMPAPI();

        public CameraClient()
        {
            this.Port = 8000;
            this.UserName = "admin";
            this.AutoRealDataCallback = true;
            this.userPtr = IntPtr.Zero;
            this.realDataCallback = new SDKTypes.FPlayESCallBack(this.RealDataCallBack);
        }

        /// <summary>
        /// 获取最后一次错误消息
        /// </summary>
        /// <returns></returns>
        public string GetLastError()
        {
            try
            {
                return SDKWrap.GetLastErrorMsg();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public override KeyValuePair<bool, string> Login()
        {
            if (this.IsLogin)
            {
                this.Logout();
            }
            try
            {
                //Sdk 初始化
                if (!this.sdkInit)
                {
                    if (!SDKWrap.NET_DVR_Init())
                    {
                        return new KeyValuePair<bool, string>(false, string.Format("初始化SDK失败,错误消息：{0}", this.GetLastError()));
                    }
                    else
                    {
                        this.sdkInit = true;
                    }
                }

                var deviceInfo = new SDKTypes.NET_DVR_DEVICEINFO_V30();

                //执行SDK登录
                int ret = SDKWrap.NET_DVR_Login_V30(this.IP, this.Port, this.UserName, this.Password, ref deviceInfo);

                if (ret < 0)
                {
                    return new KeyValuePair<bool, string>(false, string.Format("登录失败,错误消息：{0}", this.GetLastError()));
                }
                else
                {
                    this.userId = ret;
                    this.DeviceInfo = deviceInfo;
                    return new KeyValuePair<bool, string>(true, "登录成功!");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, string.Format("登录失败,异常消息：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public override KeyValuePair<bool, string> Logout()
        {
            if (!this.IsLogin)
            {
                return new KeyValuePair<bool, string>(true, "注销成功!");
            }
            //如果存在实时预览，则执行停止
            if (this.IsRealPlay)
            {
                this.StopRealPlay();
            }
            try
            {
                var ret = SDKWrap.Logout(this.userId);
                if (ret.Key)
                {
                    this.userId = -1;
                }
                return ret;
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, e.Message);
            }
        }

        #region 实时预览

        /// <summary>
        /// 启动实时预览
        /// </summary>
        /// <returns></returns>
        public override KeyValuePair<bool, string> StartRealPlay()
        {
            if (!this.IsLogin)
            {
                return new KeyValuePair<bool, string>(false, "未登录!");
            }
            if (this.IsRealPlay)
            {
                this.StopRealPlay();
            }
            try
            {
                SDKTypes.NET_DVR_PREVIEWINFO previewInfo = this.PreviewInfo;

                previewInfo.hPlayWnd = this.RealPlayWnd;

                return SDKWrap.RealPlay_V40(this.userId, ref previewInfo, ref this.realPlayHandle, IntPtr.Zero);
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, e.Message);
            }
        }

        /// <summary>
        /// 停止实时预览
        /// </summary>
        /// <returns></returns>
        public override KeyValuePair<bool, string> StopRealPlay()
        {
            if (!this.IsRealPlay)
            {
                return new KeyValuePair<bool, string>(true, "未启动实时预览!");
            }
            try
            {
                var ret = SDKWrap.StopRealPlay(this.realPlayHandle);
                if (ret.Key)
                {
                    this.realPlayHandle = IntPtr.Zero;
                }
                return ret;
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, e.Message);
            }
        }

        /// <summary>
        /// 设置实时预览回调
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<bool, string> SetRealDataCallBack()
        {
            this.realDataCallback = new SDKTypes.FPlayESCallBack(this.RealDataCallBack);

            var ret = SDKWrap.SetESRealPlayCallBack(this.realPlayHandle, this.realDataCallback, this.userPtr);

            return ret;
        }

        #endregion

        #region 回调

        /// <summary>
        /// 实时码流数据（标准码流）。
        /// </summary>
        /// <param name="lPreviewHandle"></param>
        /// <param name="pstruPackInfo"></param>
        /// <param name="dwUser"></param>
        private void RealDataCallBack(int lPreviewHandle, IntPtr pstruPackInfo, IntPtr pUser)
        {
            if(pstruPackInfo == IntPtr.Zero)
            {
                return;
            }
            //获取包结构
            var packInfo =  (SDKTypes.NET_DVR_PACKET_INFO_EX)Marshal.PtrToStructure(pstruPackInfo, typeof(SDKTypes.NET_DVR_PACKET_INFO_EX));

            if(packInfo.dwPacketType == 0)//头文件
            {

            }
            else if(packInfo.dwPacketType == 1)//I帧
            {
                if (!this.rtmpAPI.IsInit)
                {
                    this.rtmpAPI.Create();
                }
                if (!this.rtmpAPI.IsInitMetadata)
                {
                    this.rtmpAPI.Connect(new StringBuilder(this.RelayRtmpUrl));
                    this.rtmpAPI.InitMetadata(new EasyRTMP.EasyTypes.EASY_MEDIA_INFO_T());
                }
                if (this.rtmpAPI.IsInit)
                {
                    EasyRTMP.EasyTypes.EASY_AV_Frame avFrame = new EasyRTMP.EasyTypes.EASY_AV_Frame();
                    avFrame.u32AVFrameFlag = EasyRTMP.EasyTypes.EASY_SDK_VIDEO_FRAME_FLAG;
                    avFrame.u32AVFrameLen = (uint)packInfo.dwPacketSize;
                    avFrame.pBuffer = packInfo.pPacketBuffer;
                    avFrame.u32VFrameType = EasyRTMP.EasyTypes.EASY_SDK_VIDEO_FRAME_I;
                    //avFrame.u32VFrameType = Rtmp.EasyTypes.EASY_SDK_VIDEO_FRAME_P;
                    //发送数据包
                    var ret = this.rtmpAPI.SendPacket(avFrame);
                    if (ret.Key)
                    {
                        System.Diagnostics.Debug.WriteLine("send FRAME_P packages:" + packInfo.dwPacketSize.ToString());
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("send  FRAME_P failed:" + packInfo.dwPacketSize.ToString());
                    }
                }
            }
            else if(packInfo.dwPacketType == 3)
            {
                if (this.rtmpAPI.IsInit)
                {
                    EasyRTMP.EasyTypes.EASY_AV_Frame avFrame = new EasyRTMP.EasyTypes.EASY_AV_Frame();
                    avFrame.u32AVFrameFlag = EasyRTMP.EasyTypes.EASY_SDK_VIDEO_FRAME_FLAG;
                    avFrame.u32AVFrameLen = (uint)packInfo.dwPacketSize;
                    avFrame.pBuffer = packInfo.pPacketBuffer;
                    avFrame.u32VFrameType = EasyRTMP.EasyTypes.EASY_SDK_VIDEO_FRAME_I;
                    //avFrame.u32VFrameType = Rtmp.EasyTypes.EASY_SDK_VIDEO_FRAME_P;
                    //发送数据包
                    var ret = this.rtmpAPI.SendPacket(avFrame);
                    if (ret.Key)
                    {
                        System.Diagnostics.Debug.WriteLine("send FRAME_P packages:" + packInfo.dwPacketSize.ToString());
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("send  FRAME_P failed:" + packInfo.dwPacketSize.ToString());
                    }
                }
            }
        }

        #endregion

        #region 转流

        /// <summary>
        /// 启动 RTMP 转流
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<bool, string> StartRelay()
        {
            if (string.IsNullOrEmpty(this.RelayRtmpUrl))
            {
                return new KeyValuePair<bool, string>(false, "未设置转流RTMP地址!");
            }
            try
            {
                //激活 rtmp API
                if (!EasyRTMP.EasyRTMPAPI.Activated)
                {
                    var ret = EasyRTMP.EasyRTMPAPI.Activate();
                    if (!ret.Key)
                    {
                        return ret;
                    }
                }
                //初始化 rtmp API
                if (!this.rtmpAPI.IsInit)
                {
                    var ret = this.rtmpAPI.Create();
                    if (!ret.Key)
                    {
                        return ret;
                    }
                }
                return new KeyValuePair<bool, string>(true, "已启动RTMP推流!");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        /// <summary>
        /// 停止转流
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<bool, string> StopRelay()
        {
            if (!this.rtmpAPI.IsInit)
            {
                return new KeyValuePair<bool, string>(true, "RTMP未初始化!");
            }
            try
            {
                return this.rtmpAPI.Release();
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        #endregion
    }
}
