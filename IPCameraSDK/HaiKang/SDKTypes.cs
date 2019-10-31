using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace IPCameraSDK.HaiKang
{
    /// <summary>
    /// 海康SDK类型库
    /// </summary>
    public class SDKTypes
    {
        /// <summary>
        /// 序列号长度
        /// </summary>
        public const int SERIALNO_LEN = 48;

        public const int STREAM_ID_LEN = 32;

        #region NET_DVR_DEVICEINFO_V30

        /// <summary>
        /// NET_DVR_Login_V30()参数结构
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;  //序列号
            public byte byAlarmInPortNum;		        //报警输入个数
            public byte byAlarmOutPortNum;		        //报警输出个数
            public byte byDiskNum;				    //硬盘个数
            public byte byDVRType;				    //设备类型, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;				    //模拟通道个数
            public byte byStartChan;			        //起始通道号,例如DVS-1,DVR - 1
            public byte byAudioChanNum;                //语音通道数
            public byte byIPChanNum;					//最大数字通道个数，低位  
            public byte byZeroChanNum;			//零通道编码个数 //2010-01-16
            public byte byMainProto;			//主码流传输协议类型 0-private, 1-rtsp,2-同时支持private和rtsp
            public byte bySubProto;				//子码流传输协议类型0-private, 1-rtsp,2-同时支持private和rtsp
            public byte bySupport;        //能力，位与结果为0表示不支持，1表示支持，
                                          //bySupport & 0x1, 表示是否支持智能搜索
                                          //bySupport & 0x2, 表示是否支持备份
                                          //bySupport & 0x4, 表示是否支持压缩参数能力获取
                                          //bySupport & 0x8, 表示是否支持多网卡
                                          //bySupport & 0x10, 表示支持远程SADP
                                          //bySupport & 0x20, 表示支持Raid卡功能
                                          //bySupport & 0x40, 表示支持IPSAN 目录查找
                                          //bySupport & 0x80, 表示支持rtp over rtsp
            public byte bySupport1;        // 能力集扩充，位与结果为0表示不支持，1表示支持
                                           //bySupport1 & 0x1, 表示是否支持snmp v30
                                           //bySupport1 & 0x2, 支持区分回放和下载
                                           //bySupport1 & 0x4, 是否支持布防优先级	
                                           //bySupport1 & 0x8, 智能设备是否支持布防时间段扩展
                                           //bySupport1 & 0x10, 表示是否支持多磁盘数（超过33个）
                                           //bySupport1 & 0x20, 表示是否支持rtsp over http	
                                           //bySupport1 & 0x80, 表示是否支持车牌新报警信息2012-9-28, 且还表示是否支持NET_DVR_IPPARACFG_V40结构体
            public byte bySupport2; /*能力，位与结果为0表示不支持，非0表示支持							
							bySupport2 & 0x1, 表示解码器是否支持通过URL取流解码
							bySupport2 & 0x2,  表示支持FTPV40
							bySupport2 & 0x4,  表示支持ANR
							bySupport2 & 0x8,  表示支持CCD的通道参数配置
							bySupport2 & 0x10,  表示支持布防报警回传信息（仅支持抓拍机报警 新老报警结构）
							bySupport2 & 0x20,  表示是否支持单独获取设备状态子项
							bySupport2 & 0x40,  表示是否是码流加密设备*/
            public ushort wDevType;              //设备型号
            public byte bySupport3; //能力集扩展，位与结果为0表示不支持，1表示支持
                                    //bySupport3 & 0x1, 表示是否多码流
                                    // bySupport3 & 0x4 表示支持按组配置， 具体包含 通道图像参数、报警输入参数、IP报警输入、输出接入参数、
                                    // 用户参数、设备工作状态、JPEG抓图、定时和时间抓图、硬盘盘组管理 
                                    //bySupport3 & 0x8为1 表示支持使用TCP预览、UDP预览、多播预览中的"延时预览"字段来请求延时预览（后续都将使用这种方式请求延时预览）。而当bySupport3 & 0x8为0时，将使用 "私有延时预览"协议。
                                    //bySupport3 & 0x10 表示支持"获取报警主机主要状态（V40）"。
                                    //bySupport3 & 0x20 表示是否支持通过DDNS域名解析取流

            public byte byMultiStreamProto;//是否支持多码流,按位表示,0-不支持,1-支持,bit1-码流3,bit2-码流4,bit7-主码流，bit-8子码流
            public byte byStartDChan;		//起始数字通道号,0表示无效
            public byte byStartDTalkChan;	//起始数字对讲通道号，区别于模拟对讲通道号，0表示无效
            public byte byHighDChanNum;		//数字通道个数，高位
            public byte bySupport4;
            public byte byLanguageType;// 支持语种能力,按位表示,每一位0-不支持,1-支持  
                                       //  byLanguageType 等于0 表示 老设备
                                       //  byLanguageType & 0x1表示支持中文
                                       //  byLanguageType & 0x2表示支持英文
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;		//保留
        }

        #endregion

        #region NET_DVR_PREVIEWINFO

        /// <summary>
        /// 预览V40接口
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWINFO
        {
            public Int32 lChannel;//通道号
            public uint dwStreamType;	// 码流类型，0-主码流，1-子码流，2-码流3，3-码流4 等以此类推
            public uint dwLinkMode;// 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-RTP/RTSP,5-RSTP/HTTP 
            public IntPtr hPlayWnd;//播放窗口的句柄,为NULL表示不播放图象
            public bool bBlocked;  //0-非阻塞取流, 1-阻塞取流, 如果阻塞SDK内部connect失败将会有5s的超时才能够返回,不适合于轮询取流操作.
            public bool bPassbackRecord; //0-不启用录像回传,1启用录像回传
            public byte byPreviewMode;//预览模式，0-正常预览，1-延迟预览
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStreamID;//流ID，lChannel为0xffffffff时启用此参数
            public byte byProtoType; //应用层取流协议，0-私有协议，1-RTSP协议
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDisplayBufNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 216, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        #endregion

        #region NET_DVR_PACKET_INFO_EX

        /// <summary>
        /// 数据包信息结构体
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PACKET_INFO_EX
        {
            /// <summary>
            /// 宽度
            /// </summary>
            public ushort wWidth;
            /// <summary>
            /// 高度 
            /// </summary>
            public ushort wHeight;
            /// <summary>
            /// 时间戳低位 
            /// </summary>
            public uint dwTimeStamp;
            /// <summary>
            /// 时间戳高位 
            /// </summary>
            public uint dwTimeStampHigh;
            public uint dwYear;
            public uint dwMonth;
            public uint dwDay;
            public uint dwHour;
            public uint dwMinute;
            public uint dwSecond;
            public uint dwMillisecond;
            /// <summary>
            /// 帧号
            /// </summary>
            public uint dwFrameNum;
            /// <summary>
            /// 帧率，当帧率小于0时，0x80000002表示1/2帧率，同理可推0x80000010为1/16帧率
            /// </summary>
            public uint dwFrameRate;
            /// <summary>
            /// flag E帧标记
            /// </summary>
            public uint dwFlag;
            /// <summary>
            /// 帧在文件中的位置 
            /// </summary>
            public uint dwFilePos;
            /// <summary>
            /// 包类型：0- 文件头，1- 视频I帧，2- 视频B帧， 3- 视频P帧， 10- 音频数据包， 11- 私有数据包 
            /// </summary>
            public uint dwPacketType;
            /// <summary>
            /// 包数据大小
            /// </summary>
            public uint dwPacketSize;
            /// <summary>
            /// 包数据缓冲区指针 
            /// </summary>
            public IntPtr pPacketBuffer;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            /// <summary>
            /// 打包方式：0- 保留，1- FU_A打包方式
            /// </summary>
            public uint dwPacketMode;
            /// <summary>
            /// 保留，置为0 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            /// <summary>
            /// 私有数据信息，保留 
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.U4)]
            public uint[] dwReserved;
        }

        #endregion

        #region 回调委托

        /// <summary>
        /// 预览回调
        /// </summary>
        /// <param name="lRealHandle"> 当前的预览句柄 </param>
        /// <param name="dwDataType"> 数据类型 </param>
        /// <param name="pBuffer"> 存放数据的缓冲区指针 </param>
        /// <param name="dwBufSize"> 缓冲区大小 </param>
        /// <param name="pUser"> 用户数据 </param>
        public delegate void REALDATACALLBACK(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser);

        /// <summary>
        /// 预览裸码流帧数据回调函数。
        /// </summary>
        /// <param name="lPreviewHandle"></param>
        /// <param name="pstruPackInfo"></param>
        /// <param name="pUser"></param>
        public delegate void FPlayESCallBack(int lPreviewHandle, IntPtr pstruPackInfo, IntPtr pUser);

        #endregion
    }
}
