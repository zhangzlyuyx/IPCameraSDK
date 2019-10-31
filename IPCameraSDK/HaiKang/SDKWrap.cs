using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IPCameraSDK.HaiKang
{
    /// <summary>
    /// 海康SDK封装
    /// </summary>
    public class SDKWrap
    {

        #region API函数

        const string HCNetSDK_WIN = "HCNetSDK.dll";

        const string HCNetSDK_LINUX = "libhcnetsdk.so";

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_GetLastError")]
        private static extern uint NET_DVR_GetLastError_WIN();

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_GetLastError")]
        private static extern uint NET_DVR_GetLastError_LINUX();

        /// <summary>
        /// 返回最后操作的错误码
        /// </summary>
        /// <returns> 返回值为错误码。错误码主要分为网络通讯库、RTSP通讯库、软硬解库、语音对讲库等错误码 </returns>
        public static uint NET_DVR_GetLastError()
        {
            if (IsLinux())
            {
                return NET_DVR_GetLastError_LINUX();
            }
            else
            {
                return NET_DVR_GetLastError_WIN();
            }
        }

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_GetErrorMsg")]
        private static extern string NET_DVR_GetErrorMsg_WIN(ref int pErrorNo);

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_GetErrorMsg")]
        private static extern string NET_DVR_GetErrorMsg_LINUX(ref int pErrorNo);

        /// <summary>
        /// 返回最后操作的错误码信息
        /// </summary>
        /// <param name="pErrorNo"> 错误码数值的指针 </param>
        /// <returns> 返回值为错误码信息的指针。错误码主要分为网络通讯库、RTSP通讯库、软硬解库、语音对讲库等错误码 </returns>
        public static string NET_DVR_GetErrorMsg(ref int pErrorNo)
        {
            if (IsLinux())
            {
                return NET_DVR_GetErrorMsg_LINUX(ref pErrorNo);
            }
            else
            {
                return NET_DVR_GetErrorMsg_WIN(ref pErrorNo);
            }
        }

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_Init")]
        private static extern bool NET_DVR_Init_WIN();

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_Init")]
        private static extern bool NET_DVR_Init_LINUX();
        /// <summary>
        /// 初始化SDK，调用其他SDK函数的前提。
        /// </summary>
        /// <returns> TRUE表示成功，FALSE表示失败。 </returns>
        public static bool NET_DVR_Init()
        {
            if (IsLinux())
            {
                return NET_DVR_Init_LINUX();
            }
            else
            {
                return NET_DVR_Init_WIN();
            }
        }

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_Cleanup")]
        private static extern bool NET_DVR_Cleanup_WIN();

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_Cleanup")]
        private static extern bool NET_DVR_Cleanup_LINUX();

        /// <summary>
        /// 释放SDK资源，在结束之前最后调用
        /// </summary>
        /// <returns> TRUE表示成功，FALSE表示失败 </returns>
        public static bool NET_DVR_Cleanup()
        {
            if (IsLinux())
            {
                return NET_DVR_Cleanup_LINUX();
            }
            else
            {
                return NET_DVR_Cleanup_WIN();
            }
        }

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_Login_V30")]
        private static extern Int32 NET_DVR_Login_V30_WIN(string sDVRIP, Int32 wDVRPort, string sUserName, string sPassword, ref  SDKTypes.NET_DVR_DEVICEINFO_V30 lpDeviceInfo);

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_Login_V30")]
        private static extern Int32 NET_DVR_Login_V30_LINUX(string sDVRIP, Int32 wDVRPort, string sUserName, string sPassword, ref SDKTypes.NET_DVR_DEVICEINFO_V30 lpDeviceInfo);

        /// <summary>
        /// 设备登录 NET_DVR_Login_V30
        /// </summary>
        /// <param name="sDVRIP"> 设备IP地址 </param>
        /// <param name="wDVRPort"> 设备端口号 </param>
        /// <param name="sUserName"> 登录的用户名 </param>
        /// <param name="sPassword"> 用户密码 </param>
        /// <param name="lpDeviceInfo"> [out] 设备信息 </param>
        /// <returns></returns>
        public static Int32 NET_DVR_Login_V30(string sDVRIP, Int32 wDVRPort, string sUserName, string sPassword, ref SDKTypes.NET_DVR_DEVICEINFO_V30 lpDeviceInfo)
        {
            if(IsLinux())
            {
                return NET_DVR_Login_V30_LINUX(sDVRIP, wDVRPort, sUserName, sPassword, ref lpDeviceInfo);
            }
            else
            {
                return NET_DVR_Login_V30_WIN(sDVRIP, wDVRPort, sUserName, sPassword, ref lpDeviceInfo);
            }
        }

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_Logout")]
        private static extern bool NET_DVR_Logout_WIN(int iUserID);

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_Logout")]
        private static extern bool NET_DVR_Logout_LINUX(int iUserID);

        /// <summary>
        /// 设备注销
        /// </summary>
        /// <param name="iUserID"> 用户ID </param>
        /// <returns></returns>
        public static bool NET_DVR_Logout(int iUserID)
        {
            if (IsLinux())
            {
                return NET_DVR_Logout_LINUX(iUserID);
            }
            else
            {
                return NET_DVR_Logout_WIN(iUserID);
            }
        }

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_RealPlay_V40")]
        private static extern int NET_DVR_RealPlay_V40_WIN(int iUserID, ref SDKTypes.NET_DVR_PREVIEWINFO lpPreviewInfo, SDKTypes.REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser);

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_RealPlay_V40")]
        private static extern int NET_DVR_RealPlay_V40_LINUX(int iUserID, ref SDKTypes.NET_DVR_PREVIEWINFO lpPreviewInfo, SDKTypes.REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser);

        /// <summary>
        /// 实时预览扩展接口。
        /// </summary>
        /// <param name="iUserID"> NET_DVR_Login()或NET_DVR_Login_V30()的返回值 </param>
        /// <param name="lpPreviewInfo"> 预览参数 </param>
        /// <param name="fRealDataCallBack_V30"> 码流数据回调函数 </param>
        /// <param name="pUser"> 用户数据 </param>
        /// <returns></returns>
        public static int NET_DVR_RealPlay_V40(int iUserID, ref SDKTypes.NET_DVR_PREVIEWINFO lpPreviewInfo, SDKTypes.REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser)
        {
            if (IsLinux())
            {
                return NET_DVR_RealPlay_V40_LINUX(iUserID, ref lpPreviewInfo, fRealDataCallBack_V30, pUser);
            }
            else
            {
                 return NET_DVR_RealPlay_V40_WIN(iUserID, ref lpPreviewInfo, fRealDataCallBack_V30, pUser);
            }
        }

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_StopRealPlay")]
        private static extern bool NET_DVR_StopRealPlay_WIN(int iRealHandle);

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_StopRealPlay")]
        private static extern bool NET_DVR_StopRealPlay_LINUX(int iRealHandle);

        /// <summary>
        /// 停止预览
        /// </summary>
        /// <param name="iRealHandle"> 预览句柄，NET_DVR_RealPlay或者NET_DVR_RealPlay_V30的返回值 </param>
        /// <returns></returns>
        public static bool NET_DVR_StopRealPlay(int iRealHandle)
        {
            if(IsLinux())
            {
                return NET_DVR_StopRealPlay_LINUX(iRealHandle);
            }
            else
            {
                return NET_DVR_StopRealPlay_WIN(iRealHandle);
            }
        }

        [DllImport(HCNetSDK_WIN, EntryPoint = "NET_DVR_SetESRealPlayCallBack")]
        private static extern bool NET_DVR_SetESRealPlayCallBack_WIN(int lRealHandle, SDKTypes.FPlayESCallBack cbPlayESCallBack, IntPtr pUser);

        [DllImport(HCNetSDK_LINUX, EntryPoint = "NET_DVR_SetESRealPlayCallBack")]
        private static extern bool NET_DVR_SetESRealPlayCallBack_LINUX(int lRealHandle, SDKTypes.FPlayESCallBack cbPlayESCallBack, IntPtr pUser);

        /// <summary>
        /// 设置预览裸码流帧数据回调函数。
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="cbPlayESCallBack"></param>
        /// <param name="pUser"></param>
        /// <returns></returns>
        public static bool NET_DVR_SetESRealPlayCallBack(int lRealHandle, SDKTypes.FPlayESCallBack cbPlayESCallBack, IntPtr pUser)
        {
            if(IsLinux())
            {
                return NET_DVR_SetESRealPlayCallBack_LINUX(lRealHandle, cbPlayESCallBack, pUser);
            }
            else
            {
                return NET_DVR_SetESRealPlayCallBack_WIN(lRealHandle, cbPlayESCallBack, pUser);
            }
        }

        #endregion

        /// <summary>
        /// 判断是否为 Linux 系统
        /// </summary>
        /// <returns></returns>
        public static bool IsLinux()
        {
            return Environment.OSVersion.Platform == PlatformID.Unix;
        }

        public delegate void STDDATACALLBACK(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        /// <summary>
        /// 错误代码字典
        /// </summary>
        private static Dictionary<int, string> errorCodeDict = new Dictionary<int, string>();

        /// <summary>
        /// 获取错误代码
        /// </summary>
        /// <returns></returns>
        public static int GetLastErrorCode()
        {
            try
            {
                var errorCode = GetLastErrorCode();
                return (int)errorCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <returns></returns>
        public static string GetLastErrorMsg()
        {
            int code = GetLastErrorCode();
            if(errorCodeDict.Count == 0)
            {
                foreach (var field in typeof(ErrorCodes).GetFields())
                {
                    if (!field.IsStatic)
                    {
                        continue;
                    }
                    if(field.FieldType != typeof(int))
                    {
                        continue;
                    }
                    object[] attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if(attributes != null && attributes.Length > 0)
                    {
                        int value = (int)field.GetValue(null);
                        string description = (attributes[0] as DescriptionAttribute).Description;
                        errorCodeDict[value] = description;
                    }
                }
            }
            if (errorCodeDict.ContainsKey(code))
            {
                return errorCodeDict[code];
            }
            return code.ToString();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<bool, string> Init()
        {
            try
            {
                if (!NET_DVR_Init())
                {
                    return new KeyValuePair<bool, string>(false, string.Format("SDK初始化失败,错误消息：{0}", GetLastErrorMsg()));
                }
                return new KeyValuePair<bool, string>(true, "SDK初始化成功!");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, string.Format("SDK初始化失败,异常消息：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="userId"> 用户id </param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> Logout(int userId)
        {
            try
            {
                if (!NET_DVR_Logout(userId))
                {
                    return new KeyValuePair<bool, string>(false, string.Format("注销失败,错误消息：{0}", GetLastErrorMsg()));
                }
                else
                {
                    return new KeyValuePair<bool, string>(true, "注销成功!");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, string.Format("注销失败,异常消息：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 启动实时预览（支持多码流）。
        /// </summary>
        /// <param name="userId"> 用户id </param>
        /// <param name="previewInfo"> 预览参数 </param>
        /// <param name="realPlayHandle"> 预览句柄 </param>
        /// <param name="userPtr"> 用户数据 </param>
        /// <param name="fRealDataCallBack_V30"></param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> RealPlay_V40(int userId, ref SDKTypes.NET_DVR_PREVIEWINFO previewInfo, ref IntPtr realPlayHandle, IntPtr userPtr, SDKTypes.REALDATACALLBACK fRealDataCallBack_V30 = null)
        {
            try
            {
                int handle = NET_DVR_RealPlay_V40(userId, ref previewInfo, fRealDataCallBack_V30, userPtr);
                if(handle == -1)
                {
                    return new KeyValuePair<bool, string>(false, string.Format("启动实时预览失败,错误消息：{0}", GetLastErrorMsg()));
                }
                else
                {
                    realPlayHandle = new IntPtr(handle);
                    return new KeyValuePair<bool, string>(true, string.Format("启动实时预览成功,预览句柄：{0}",handle));
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, string.Format("启动实时预览失败,异常消息：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 设置预览裸码流帧数据回调函数。
        /// </summary>
        /// <param name="realPlayHandle"></param>
        /// <param name="fPlayESCallBack"></param>
        /// <param name="userPtr"></param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> SetESRealPlayCallBack(IntPtr realPlayHandle, SDKTypes.FPlayESCallBack fPlayESCallBack, IntPtr userPtr)
        {
            try
            {
                if (!NET_DVR_SetESRealPlayCallBack(realPlayHandle.ToInt32(), fPlayESCallBack, userPtr))
                {
                    return new KeyValuePair<bool, string>(false, string.Format("设置实时预览回调失败,错误消息：{0}", GetLastErrorMsg()));
                }
                else
                {
                    return new KeyValuePair<bool, string>(true, "设置实时预览回调成功!");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, string.Format("设置实时预览回调失败,异常消息：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 停止实时预览
        /// </summary>
        /// <param name="realPlayHandle"> 实时预览句柄 </param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> StopRealPlay(IntPtr realPlayHandle)
        {
            try
            {
                if (!NET_DVR_StopRealPlay(realPlayHandle.ToInt32()))
                {
                    return new KeyValuePair<bool, string>(false, string.Format("停止实时预览失败,错误消息：{0}", GetLastErrorMsg()));
                }
                else
                {
                    return new KeyValuePair<bool, string>(true, "停止实时预览成功!");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, string.Format("停止实时预览失败,异常消息：{0}",ex.Message));
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<bool, string> Cleanup()
        {
            try
            {
                if (!NET_DVR_Cleanup())
                {
                    return new KeyValuePair<bool, string>(false, string.Format("释放资源失败,错误消息：{0}", GetLastErrorMsg()));
                }
                else
                {
                    return new KeyValuePair<bool, string>(true, "释放资源成功!");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, string.Format("释放资源失败,异常消息：{0}", ex.Message));
            }
        }

        #region ErrorCodes

        /// <summary>
        /// 全局错误代码
        /// </summary>
        public class ErrorCodes
        {
            /// <summary>
            /// 没有错误
            /// </summary>
            [Description("没有错误")]
            public const int NET_DVR_NOERROR = 0;
            /// <summary>
            /// 用户名密码错误
            /// </summary>
            [Description("用户名密码错误")]
            public const int NET_DVR_PASSWORD_ERROR = 1;
            [Description("权限不足")]
            public const int NET_DVR_NOENOUGHPRI = 2;//权限不足
            [Description("没有初始化")]
            public const int NET_DVR_NOINIT = 3;//没有初始化
            [Description("通道号错误")]
            public const int NET_DVR_CHANNEL_ERROR = 4;//通道号错误
            [Description("连接到DVR的客户端个数超过最大")]
            public const int NET_DVR_OVER_MAXLINK = 5;//连接到DVR的客户端个数超过最大
            [Description("版本不匹配")]
            public const int NET_DVR_VERSIONNOMATCH = 6;//版本不匹配
            [Description("连接服务器失败")]
            public const int NET_DVR_NETWORK_FAIL_CONNECT = 7;//连接服务器失败
            [Description("向服务器发送失败")]
            public const int NET_DVR_NETWORK_SEND_ERROR = 8;//向服务器发送失败
            [Description("从服务器接收数据失败")]
            public const int NET_DVR_NETWORK_RECV_ERROR = 9;//从服务器接收数据失败
            [Description("从服务器接收数据超时")]
            public const int NET_DVR_NETWORK_RECV_TIMEOUT = 10;//从服务器接收数据超时
            [Description("传送的数据有误")]
            public const int NET_DVR_NETWORK_ERRORDATA = 11;//传送的数据有误
            [Description("调用次序错误")]
            public const int NET_DVR_ORDER_ERROR = 12;//调用次序错误
            [Description("无此权限")]
            public const int NET_DVR_OPERNOPERMIT = 13;//无此权限
            [Description("DVR命令执行超时")]
            public const int NET_DVR_COMMANDTIMEOUT = 14;//DVR命令执行超时
            [Description("串口号错误")]
            public const int NET_DVR_ERRORSERIALPORT = 15;//串口号错误
            [Description("报警端口错误")]
            public const int NET_DVR_ERRORALARMPORT = 16;//报警端口错误
            [Description("参数错误")]
            public const int NET_DVR_PARAMETER_ERROR = 17;//参数错误
            [Description("服务器通道处于错误状态")]
            public const int NET_DVR_CHAN_EXCEPTION = 18;//服务器通道处于错误状态
            [Description("没有硬盘")]
            public const int NET_DVR_NODISK = 19;//没有硬盘
            [Description("硬盘号错误")]
            public const int NET_DVR_ERRORDISKNUM = 20;//硬盘号错误
            [Description("服务器硬盘满")]
            public const int NET_DVR_DISK_FULL = 21;//服务器硬盘满
            [Description("服务器硬盘出错")]
            public const int NET_DVR_DISK_ERROR = 22;//服务器硬盘出错
            [Description("服务器不支持")]
            public const int NET_DVR_NOSUPPORT = 23;//服务器不支持
            [Description("服务器忙")]
            public const int NET_DVR_BUSY = 24;//服务器忙
            [Description("服务器修改不成功")]
            public const int NET_DVR_MODIFY_FAIL = 25;//服务器修改不成功
            [Description("密码输入格式不正确")]
            public const int NET_DVR_PASSWORD_FORMAT_ERROR = 26;//密码输入格式不正确
            [Description("硬盘正在格式化，不能启动操作")]
            public const int NET_DVR_DISK_FORMATING = 27;//硬盘正在格式化，不能启动操作
            [Description("DVR资源不足")]
            public const int NET_DVR_DVRNORESOURCE = 28;//DVR资源不足
            [Description("DVR操作失败")]
            public const int NET_DVR_DVROPRATEFAILED = 29;//DVR操作失败
            [Description("打开PC声音失败")]
            public const int NET_DVR_OPENHOSTSOUND_FAIL = 30;//打开PC声音失败
            [Description("服务器语音对讲被占用")]
            public const int NET_DVR_DVRVOICEOPENED = 31;//服务器语音对讲被占用
            [Description("时间输入不正确")]
            public const int NET_DVR_TIMEINPUTERROR = 32;//时间输入不正确
            [Description("回放时服务器没有指定的文件")]
            public const int NET_DVR_NOSPECFILE = 33;//回放时服务器没有指定的文件
            [Description("创建文件出错")]
            public const int NET_DVR_CREATEFILE_ERROR = 34;//创建文件出错
            [Description("打开文件出错")]
            public const int NET_DVR_FILEOPENFAIL = 35;//打开文件出错
            [Description("上次的操作还没有完成")]
            public const int NET_DVR_OPERNOTFINISH = 36; //上次的操作还没有完成
            [Description("获取当前播放的时间出错")]
            public const int NET_DVR_GETPLAYTIMEFAIL = 37;//获取当前播放的时间出错
            [Description("播放出错")]
            public const int NET_DVR_PLAYFAIL = 38;//播放出错
            [Description("文件格式不正确")]
            public const int NET_DVR_FILEFORMAT_ERROR = 39;//文件格式不正确
            [Description("路径错误")]
            public const int NET_DVR_DIR_ERROR = 40;//路径错误
            [Description("资源分配错误")]
            public const int NET_DVR_ALLOC_RESOURCE_ERROR = 41;//资源分配错误
            [Description("声卡模式错误")]
            public const int NET_DVR_AUDIO_MODE_ERROR = 42;//声卡模式错误
            [Description("缓冲区太小")]
            public const int NET_DVR_NOENOUGH_BUF = 43;//缓冲区太小
            [Description("创建SOCKET出错")]
            public const int NET_DVR_CREATESOCKET_ERROR = 44;//创建SOCKET出错
            [Description("设置SOCKET出错")]
            public const int NET_DVR_SETSOCKET_ERROR = 45;//设置SOCKET出错
            [Description("个数达到最大")]
            public const int NET_DVR_MAX_NUM = 46;//个数达到最大
            [Description("用户不存在")]
            public const int NET_DVR_USERNOTEXIST = 47;//用户不存在
            [Description("写FLASH出错")]
            public const int NET_DVR_WRITEFLASHERROR = 48;//写FLASH出错
            [Description("DVR升级失败")]
            public const int NET_DVR_UPGRADEFAIL = 49;//DVR升级失败
            [Description("解码卡已经初始化过")]
            public const int NET_DVR_CARDHAVEINIT = 50;//解码卡已经初始化过
            [Description("调用播放库中某个函数失败")]
            public const int NET_DVR_PLAYERFAILED = 51;//调用播放库中某个函数失败
            [Description("设备端用户数达到最大")]
            public const int NET_DVR_MAX_USERNUM = 52;//设备端用户数达到最大
            [Description("")]
            public const int NET_DVR_GETLOCALIPANDMACFAIL = 53;//获得客户端的IP地址或物理地址失败
            [Description("")]
            public const int NET_DVR_NOENCODEING = 54;//该通道没有编码
            [Description("")]
            public const int NET_DVR_IPMISMATCH = 55;//IP地址不匹配
            [Description("")]
            public const int NET_DVR_MACMISMATCH = 56;//MAC地址不匹配
            [Description("")]
            public const int NET_DVR_UPGRADELANGMISMATCH = 57;//升级文件语言不匹配
            [Description("")]
            public const int NET_DVR_MAX_PLAYERPORT = 58;//播放器路数达到最大
            [Description("")]
            public const int NET_DVR_NOSPACEBACKUP = 59;//备份设备中没有足够空间进行备份
            [Description("")]
            public const int NET_DVR_NODEVICEBACKUP = 60;//没有找到指定的备份设备
            [Description("")]
            public const int NET_DVR_PICTURE_BITS_ERROR = 61;//图像素位数不符，限24色
            [Description("")]
            public const int NET_DVR_PICTURE_DIMENSION_ERROR = 62;//图片高*宽超限， 限128*256
            [Description("")]
            public const int NET_DVR_PICTURE_SIZ_ERROR = 63;//图片大小超限，限100K
            [Description("")]
            public const int NET_DVR_LOADPLAYERSDKFAILED = 64;//载入当前目录下Player Sdk出错
            [Description("")]
            public const int NET_DVR_LOADPLAYERSDKPROC_ERROR = 65;//找不到Player Sdk中某个函数入口
            [Description("")]
            public const int NET_DVR_LOADDSSDKFAILED = 66;//载入当前目录下DSsdk出错
            [Description("")]
            public const int NET_DVR_LOADDSSDKPROC_ERROR = 67;//找不到DsSdk中某个函数入口
            [Description("")]
            public const int NET_DVR_DSSDK_ERROR = 68;//调用硬解码库DsSdk中某个函数失败
            [Description("")]
            public const int NET_DVR_VOICEMONOPOLIZE = 69;//声卡被独占
            [Description("")]
            public const int NET_DVR_JOINMULTICASTFAILED = 70;//加入多播组失败
            [Description("")]
            public const int NET_DVR_CREATEDIR_ERROR = 71;//建立日志文件目录失败
            [Description("")]
            public const int NET_DVR_BINDSOCKET_ERROR = 72;//绑定套接字失败
            [Description("")]
            public const int NET_DVR_SOCKETCLOSE_ERROR = 73;//socket连接中断，此错误通常是由于连接中断或目的地不可达
            [Description("")]
            public const int NET_DVR_USERID_ISUSING = 74;//注销时用户ID正在进行某操作
            [Description("")]
            public const int NET_DVR_SOCKETLISTEN_ERROR = 75;//监听失败
            [Description("")]
            public const int NET_DVR_PROGRAM_EXCEPTION = 76;//程序异常
            [Description("")]
            public const int NET_DVR_WRITEFILE_FAILED = 77;//写文件失败
            [Description("")]
            public const int NET_DVR_FORMAT_READONLY = 78;//禁止格式化只读硬盘
            [Description("")]
            public const int NET_DVR_WITHSAMEUSERNAME = 79;//用户配置结构中存在相同的用户名
            [Description("")]
            public const int NET_DVR_DEVICETYPE_ERROR = 80;//导入参数时设备型号不匹配
            [Description("")]
            public const int NET_DVR_LANGUAGE_ERROR = 81;//导入参数时语言不匹配
            [Description("")]
            public const int NET_DVR_PARAVERSION_ERROR = 82;//导入参数时软件版本不匹配
            [Description("")]
            public const int NET_DVR_IPCHAN_NOTALIVE = 83; //预览时外接IP通道不在线
            [Description("")]
            public const int NET_DVR_RTSP_SDK_ERROR = 84;//加载高清IPC通讯库StreamTransClient.dll失败
            [Description("")]
            public const int NET_DVR_CONVERT_SDK_ERROR = 85;//加载转码库失败
            [Description("")]
            public const int NET_DVR_IPC_COUNT_OVERFLOW = 86;//超出最大的ip接入通道数
            [Description("")]
            public const int NET_PLAYM4_NOERROR = 500;//no error
            public const int NET_PLAYM4_PARA_OVER = 501;//input parameter is invalid
            public const int NET_PLAYM4_ORDER_ERROR = 502;//The order of the function to be called is error
            public const int NET_PLAYM4_TIMER_ERROR = 503;//Create multimedia clock failed
            public const int NET_PLAYM4_DEC_VIDEO_ERROR = 504;//Decode video data failed
            public const int NET_PLAYM4_DEC_AUDIO_ERROR = 505;//Decode audio data failed
            public const int NET_PLAYM4_ALLOC_MEMORY_ERROR = 506;//Allocate memory failed
            public const int NET_PLAYM4_OPEN_FILE_ERROR = 507;//Open the file failed
            public const int NET_PLAYM4_CREATE_OBJ_ERROR = 508;//Create thread or event failed
            public const int NET_PLAYM4_CREATE_DDRAW_ERROR = 509;//Create DirectDraw object failed
            public const int NET_PLAYM4_CREATE_OFFSCREEN_ERROR = 510;//failed when creating off-screen surface
            public const int NET_PLAYM4_BUF_OVER = 511;//buffer is overflow
            public const int NET_PLAYM4_CREATE_SOUND_ERROR = 512;//failed when creating audio device
            public const int NET_PLAYM4_SET_VOLUME_ERROR = 513;//Set volume failed
            public const int NET_PLAYM4_SUPPORT_FILE_ONLY = 514;//The function only support play file
            public const int NET_PLAYM4_SUPPORT_STREAM_ONLY = 515;//The function only support play stream
            public const int NET_PLAYM4_SYS_NOT_SUPPORT = 516;//System not support
            public const int NET_PLAYM4_FILEHEADER_UNKNOWN = 517;//No file header
            public const int NET_PLAYM4_VERSION_INCORRECT = 518;//The version of decoder and encoder is not adapted
            public const int NET_PALYM4_INIT_DECODER_ERROR = 519;//Initialize decoder failed
            public const int NET_PLAYM4_CHECK_FILE_ERROR = 520;//The file data is unknown
            public const int NET_PLAYM4_INIT_TIMER_ERROR = 521;//Initialize multimedia clock failed
            public const int NET_PLAYM4_BLT_ERROR = 522;//Blt failed
            public const int NET_PLAYM4_UPDATE_ERROR = 523;//Update failed
            public const int NET_PLAYM4_OPEN_FILE_ERROR_MULTI = 524;//openfile error, streamtype is multi
            public const int NET_PLAYM4_OPEN_FILE_ERROR_VIDEO = 525;//openfile error, streamtype is video
            public const int NET_PLAYM4_JPEG_COMPRESS_ERROR = 526;//JPEG compress error
            public const int NET_PLAYM4_EXTRACT_NOT_SUPPORT = 527;//Don't support the version of this file
            public const int NET_PLAYM4_EXTRACT_DATA_ERROR = 528;//extract video data failed
        }

        #endregion
    }
}
