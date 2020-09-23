// Decompiled with JetBrains decompiler
// Type: Abrivision.AbriDoorSDK
// Assembly: AbriDoorSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A9D7DFFD-13DF-4DB8-AD5B-44DE80DED6AF
// Assembly location: D:\GSoft\QLNS_1\HRPro7\AbriDoorSDK.dll

using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using zkemkeeper;

namespace TestDeviceAbriDoor
{
    public delegate void OnVerify(int userID, string pIP);
    public delegate void OnHIDNum(int pCardNumber, string pIP);
    public delegate void OnAttTransactionEx(int EnrollNumber, string pIP, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second);
    public class AbriDoorSDK
    {
        public CZKEM m_engine = null;
        public bool m_connected = false;
        private int m_VerifyMode = 0;
        public string m_IP = "";
        private ManualResetEvent m_eventStopThread;
        private ManualResetEvent m_eventEngineCreated;

        public bool IsConnected
        {
            get
            {
                return !string.IsNullOrEmpty(this.IPAdress) && this.CheckPing() && this.m_connected;
            }
        }

        private bool CheckPing()
        {
            try
            {
                try
                {
                    IPAddress.Parse(this.IPAdress);
                }
                catch
                {
                    return true;
                }
                return new Ping().Send(this.IPAdress, 500).Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        public string IPAdress
        {
            get
            {
                return this.m_IP;
            }
        }

        public event OnVerify OnVerifyEventHandler;

        public event OnHIDNum OnHIDNumEventHandler;

        public event OnAttTransactionEx OnAttTransactionExEventHandler;

        private static AbriDoorSDK instance = new AbriDoorSDK();
        public static AbriDoorSDK Instance
        {
            get
            {
                if (instance == null) instance = new AbriDoorSDK();
                return instance;
            }
        }

        public AbriDoorSDK()
        {
            this.CreateEngines();
        }

        public void ReleaseThread()
        {
        }

        public bool Beep(int ms)
        {
            return this.m_engine.Beep(100);
        }

        public bool ACUnlock(int ms)
        {
            return this.m_engine.ACUnlock(this.m_engine.MachineNumber, ms);
        }

        public int ACCGroup()
        {
            return this.m_engine.AccGroup;
        }

        public bool BackupData(string dwDataFile)
        {
            return this.m_engine.BackupData(dwDataFile);
        }

        public int BASE64()
        {
            return this.m_engine.BASE64;
        }

        public bool BatchUpdate()
        {
            return this.m_engine.BatchUpdate(this.m_engine.MachineNumber);
        }

        public bool BeginBatchUpdate(int dwUpdateFlag)
        {
            return this.m_engine.BeginBatchUpdate(this.m_engine.MachineNumber, dwUpdateFlag);
        }

        public bool CancelOperation()
        {
            return this.m_engine.CancelOperation();
        }

        public bool CaptureImage(
          bool FullImage,
          ref int Width,
          ref int Height,
          ref byte Image,
          string ImageFile)
        {
            return this.m_engine.CaptureImage(FullImage, ref Width, ref Height, ref Image, ImageFile);
        }

        public bool ClearAdministrators()
        {
            return this.m_engine.ClearAdministrators(this.m_engine.MachineNumber);
        }

        public bool ClearData(int dwDataFlag)
        {
            return this.m_engine.ClearData(this.m_engine.MachineNumber, dwDataFlag);
        }

        public bool ClearGLog()
        {
            return this.m_engine.ClearGLog(this.m_engine.MachineNumber);
        }

        public bool ClearKeeperData()
        {
            return this.m_engine.ClearKeeperData(this.m_engine.MachineNumber);
        }

        public bool ClearLCD()
        {
            return this.m_engine.ClearLCD();
        }

        public bool ClearSLog()
        {
            return this.m_engine.ClearSLog(this.m_engine.MachineNumber);
        }

        public bool ClearSMS()
        {
            return this.m_engine.ClearSMS(this.m_engine.MachineNumber);
        }

        public bool ClearUserSMS()
        {
            return this.m_engine.ClearUserSMS(this.m_engine.MachineNumber);
        }

        public bool ClearWorkCode()
        {
            return this.m_engine.ClearWorkCode();
        }

        public int CommPort()
        {
            return this.m_engine.CommPort;
        }

        public bool Connect_Com(int Comport, int BaudRate)
        {
            return this.m_engine.Connect_Com(Comport, this.m_engine.MachineNumber, BaudRate);
        }

        public bool Connect_Modem(int ComPort, int BaudRate, string strTelephone)
        {
            return this.m_engine.Connect_Modem(ComPort, this.m_engine.MachineNumber, BaudRate, strTelephone);
        }

        public bool Connect_Net(string IPAdd, int Port)
        {
            this.m_IP = IPAdd;
            if (this.CheckPing())
                this.m_connected = this.m_engine.Connect_Net(IPAdd, Port);
            return this.m_connected;
        }

        public bool CheckConnect(string IPAdd, int Port)
        {
            this.m_IP = IPAdd;
            return this.CheckPing();
        }

        public bool Connect_USB()
        {
            return this.m_engine.Connect_USB(this.m_engine.MachineNumber);
        }

        public int ConvertBIG5()
        {
            return this.m_engine.ConvertBIG5;
        }

        public void ConvertPassword(int dwSrcPSW, ref int dwDestPSW, int dwLength)
        {
            this.m_engine.ConvertPassword(dwSrcPSW, ref dwDestPSW, dwLength);
        }

        public bool DeleteAttlogBetweenTheDate(
          int dwStartYear,
          int dwStartMonth,
          int dwStartDay,
          int dwStartHour,
          int dwStartMin,
          int dwStartSec,
          int dwEndYear,
          int dwEndMonth,
          int dwEndDay,
          int dwEndHour,
          int dwEndMin,
          int dwEndSec)
        {
            if (this.IsTFTMachine())
            {
                if (this.m_engine.GetType().GetMethod(nameof(DeleteAttlogBetweenTheDate)) == null)
                    throw new Exception("this zkeem DON'T support for DeleteAttlogBetweenTheDate function");
                return this.m_engine.DeleteAttlogBetweenTheDate(this.m_engine.MachineNumber, dwStartYear, dwStartMonth, dwStartDay, dwStartHour, dwStartMin, dwStartSec, dwEndYear, dwEndMonth, dwEndDay, dwEndHour, dwEndMin, dwEndSec);
            }
            if (this.m_engine.GetType().GetMethod("DeleteAttlogBetweenTheDateBW") == null)
                throw new Exception("this zkeem DON'T support for DeleteAttlogBetweenTheDateBW function");
            return this.m_engine.DeleteAttlogBetweenTheDateBW(this.m_engine.MachineNumber, 0, dwStartYear, dwStartMonth, dwStartDay, dwStartHour, dwStartMin, dwStartSec, dwEndYear, dwEndMonth, dwEndDay, dwEndHour, dwEndMin, dwEndSec);
        }

        public bool DelCustomizeAttState(int StateID)
        {
            return this.m_engine.DelCustomizeAttState(this.m_engine.MachineNumber, StateID);
        }

        public bool DelCustomizeVoice(int VoiceID)
        {
            return this.m_engine.DelCustomizeVoice(this.m_engine.MachineNumber, VoiceID);
        }

        public bool DeleteEnrollData(int dwEnrollNumber, int dwEMachineNumber, int dwBackupNumber)
        {
            return this.m_engine.DeleteEnrollData(this.m_engine.MachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber);
        }

        public bool DeleteSMS(int ID)
        {
            return this.m_engine.DeleteSMS(this.m_engine.MachineNumber, ID);
        }

        public bool DeleteUserInfoEx(int dwEnrollNumber)
        {
            return this.m_engine.DeleteUserInfoEx(this.m_engine.MachineNumber, dwEnrollNumber);
        }

        public bool DeleteUserSMS(int dwEnrollNumber, int SMSID)
        {
            return this.m_engine.DeleteUserSMS(this.m_engine.MachineNumber, dwEnrollNumber, SMSID);
        }

        public bool DeleteWorkCode(int WorkCodeID)
        {
            return this.m_engine.DeleteWorkCode(WorkCodeID);
        }

        public bool DelUserTmp(int dwEnrollNumber, int dwFingerIndex)
        {
            return this.m_engine.DelUserTmp(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex);
        }

        public bool DisableDeviceWithTimeOut(int TimeOutSec)
        {
            return this.m_engine.DisableDeviceWithTimeOut(this.m_engine.MachineNumber, TimeOutSec);
        }

        public void Disconnect()
        {
            if (this.m_engine != null)
            {
                this.m_engine.Disconnect();
                this.m_connected = false;
            }
        }

        public bool DownloadUserPhoto(string pFileName, string pDirectoryPath)
        {
            return this.m_engine.DownloadUserPhoto(this.m_engine.MachineNumber, pFileName, pDirectoryPath);
        }

        public bool EmptyCard()
        {
            return this.m_engine.EmptyCard(this.m_engine.MachineNumber);
        }

        public bool EnableClock(int Enabled)
        {
            return this.m_engine.EnableClock(Enabled);
        }

        public bool EnableCustomizeAttState(int StateID, int Enable)
        {
            return this.m_engine.EnableCustomizeAttState(this.m_engine.MachineNumber, StateID, Enable);
        }

        public bool EnableCustomizeVoice(int VoiceID, int Enable)
        {
            return this.m_engine.EnableCustomizeVoice(this.m_engine.MachineNumber, VoiceID, Enable);
        }

        public bool EnableDevice(bool bFlag)
        {
            return this.m_engine.EnableDevice(this.m_engine.MachineNumber, bFlag);
        }

        public bool EnableUser(
          int dwEnrollNumber,
          int dwEMachineNumber,
          int dwBackupNumber,
          bool bFlag)
        {
            return this.m_engine.EnableUser(this.m_engine.MachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, bFlag);
        }

        public bool FPTempConvert(ref byte TmpData1, ref byte TmpData2, ref int Size)
        {
            return this.m_engine.FPTempConvert(ref TmpData1, ref TmpData2, ref Size);
        }

        public bool FPTempConvertNew(ref byte TmpData1, ref byte TmpData2, ref int Size)
        {
            return this.m_engine.FPTempConvertNew(ref TmpData1, ref TmpData2, ref Size);
        }

        public bool FPTempConvertNewStr(string TmpData1, ref string TmpData2, ref int Size)
        {
            return this.m_engine.FPTempConvertNewStr(TmpData1, ref TmpData2, ref Size);
        }

        public bool FPTempConvertStr(string TmpData1, ref string TmpData2, ref int Size)
        {
            return this.m_engine.FPTempConvertStr(TmpData1, ref TmpData2, ref Size);
        }

        public int get_AccTimezones(int Index)
        {
            return this.m_engine.get_AccTimeZones(Index);
        }

        public int get_CardNumber(int Index)
        {
            return this.m_engine.get_CardNumber(Index);
        }

        public string get_STR_CardNumber(int Index)
        {
            return this.m_engine.get_STR_CardNumber(Index);
        }

        public bool GetACFun(ref int ACFun)
        {
            return this.m_engine.GetACFun(ref ACFun);
        }

        public bool GetAllGLogData(
          ref int dwTMachineNumber,
          ref int dwEnrollNumber,
          ref int dwEMachineNumber,
          ref int dwVerifyMode,
          ref int dwInOutMode,
          ref int dwYear,
          ref int dwMonth,
          ref int dwDay,
          ref int dwHour,
          ref int dwMinute)
        {
            return this.m_engine.GetAllGLogData(this.m_engine.MachineNumber, ref dwTMachineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwVerifyMode, ref dwInOutMode, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute);
        }

        public bool GetAllSLogData(
          ref int dwTMachineNumber,
          ref int dwSEnrollNumber,
          ref int Params4,
          ref int Params1,
          ref int Params2,
          ref int dwManipulation,
          ref int Params3,
          ref int dwYear,
          ref int dwMonth,
          ref int dwDay,
          ref int dwHour,
          ref int dwMinute)
        {
            return this.m_engine.GetAllSLogData(this.m_engine.MachineNumber, ref dwTMachineNumber, ref dwSEnrollNumber, ref Params4, ref Params1, ref Params2, ref dwManipulation, ref Params3, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute);
        }

        public bool GetAllUserID(
          ref int dwEnrollNumber,
          ref int dwEMachineNumber,
          ref int dwBackupNumber,
          ref int dwMachinePrivilege,
          ref int dwEnable)
        {
            return this.m_engine.GetAllUserID(this.m_engine.MachineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwBackupNumber, ref dwMachinePrivilege, ref dwEnable);
        }

        public bool GetAllUserInfo(
          ref int dwEnrollNumber,
          ref string Name,
          ref string Password,
          ref int Privilege,
          ref bool Enabled)
        {
            return this.m_engine.GetAllUserInfo(this.m_engine.MachineNumber, ref dwEnrollNumber, ref Name, ref Password, ref Privilege, ref Enabled);
        }

        public int GetBackupNumber()
        {
            return this.m_engine.GetBackupNumber(this.m_engine.MachineNumber);
        }

        public bool GetBlackListPhotos(string pDirectoryPath, ref string pFileNames)
        {
            return this.m_engine.GetBlacklistPhotos(this.m_engine.MachineNumber, pDirectoryPath, ref pFileNames);
        }

        public bool GetCapturePhotos(string pDirectoryPath, ref string pFileNames)
        {
            return this.m_engine.GetCapturePhotos(this.m_engine.MachineNumber, pDirectoryPath, ref pFileNames);
        }

        public bool GetCardFun(ref int CardFun)
        {
            return this.m_engine.GetCardFun(this.m_engine.MachineNumber, ref CardFun);
        }

        public bool GetDataFile(int DataFlag, string FileName)
        {
            return this.m_engine.GetDataFile(this.m_engine.MachineNumber, DataFlag, FileName);
        }

        public bool GetDeviceInfo(int dwInfo, ref int dwValue)
        {
            return this.m_engine.GetDeviceInfo(this.m_engine.MachineNumber, dwInfo, ref dwValue);
        }

        public bool GetDeviceIP(ref string IPAddr)
        {
            return this.m_engine.GetDeviceIP(this.m_engine.MachineNumber, ref IPAddr);
        }

        public bool GetDeviceMAC(ref string sMAC)
        {
            return this.m_engine.GetDeviceMAC(this.m_engine.MachineNumber, ref sMAC);
        }

        public bool GetDeviceStatus(int dwStatus, ref int dwValue)
        {
            return this.m_engine.GetDeviceStatus(this.m_engine.MachineNumber, dwStatus, ref dwValue);
        }

        public bool GetDeviceStrInfo(int dwInfo, out string Value)
        {
            return this.m_engine.GetDeviceStrInfo(this.m_engine.MachineNumber, dwInfo, out Value);
        }

        public bool GetDeviceTime(
          ref int dwYear,
          ref int dwMonth,
          ref int dwDay,
          ref int dwHour,
          ref int dwMinute,
          ref int dwSecond)
        {
            return this.m_engine.GetDeviceTime(this.m_engine.MachineNumber, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute, ref dwSecond);
        }

        public bool GetDoorState(ref int State)
        {
            return this.m_engine.GetDoorState(this.m_engine.MachineNumber, ref State);
        }

        public bool GetEnrollData(
          int dwEnrollNumber,
          int dwEMachineNumber,
          int dwBackupNumber,
          ref int dwMachinePrivilege,
          ref int dwEnrollData,
          ref int dwPassword)
        {
            return this.m_engine.GetEnrollData(this.m_engine.MachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, ref dwMachinePrivilege, ref dwEnrollData, ref dwPassword);
        }

        public bool GetEnrollDataStr(
          int dwEnrollNumber,
          int dwEMachineNumber,
          int dwBackupNumber,
          ref int dwMachinePrivilege,
          ref string dwEnrollData,
          ref int dwPassWord)
        {
            return this.m_engine.GetEnrollDataStr(this.m_engine.MachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, ref dwMachinePrivilege, ref dwEnrollData, ref dwPassWord);
        }

        public bool GetFirmwareVersion(ref string strVersion)
        {
            return this.m_engine.GetFirmwareVersion(this.m_engine.MachineNumber, ref strVersion);
        }

        public int GetFPTempLength(ref byte dwEnrollData)
        {
            return this.m_engine.GetFPTempLength(ref dwEnrollData);
        }

        public int GetFPTempLengthStr(string dwEnrollData)
        {
            return this.m_engine.GetFPTempLengthStr(dwEnrollData);
        }

        public bool GetGeneralExtLogData(
          ref int dwEnrollNumber,
          ref int dwVerifyMode,
          ref int dwInOutMode,
          ref int dwYear,
          ref int dwMonth,
          ref int dwDay,
          ref int dwHour,
          ref int dwMinute,
          ref int dwSecond,
          ref int dwWorkCode,
          ref int dwReserved)
        {
            return this.m_engine.GetGeneralExtLogData(this.m_engine.MachineNumber, ref dwEnrollNumber, ref dwVerifyMode, ref dwInOutMode, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute, ref dwSecond, ref dwWorkCode, ref dwReserved);
        }

        public bool GetGeneralLogData(
          ref int dwTMachineNumber,
          ref int dwEnrollNumber,
          ref int dwEMachineNumber,
          ref int dwVerifyMode,
          ref int dwInOutMode,
          ref int dwYear,
          ref int dwMonth,
          ref int dwDay,
          ref int dwHour,
          ref int dwMinute)
        {
            return this.m_engine.GetGeneralLogData(this.m_engine.MachineNumber, ref dwTMachineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwVerifyMode, ref dwInOutMode, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute);
        }

        public bool GetGeneralLogDataStr(
          ref int dwEnrollNumber,
          ref int dwVerifyMode,
          ref int dwInOutMode,
          ref string TimeStr)
        {
            return this.m_engine.GetGeneralLogDataStr(this.m_engine.MachineNumber, ref dwEnrollNumber, ref dwVerifyMode, ref dwInOutMode, ref TimeStr);
        }

        public bool GetGroupTZs(int GroupIndex, ref int TZs)
        {
            return this.m_engine.GetGroupTZs(this.m_engine.MachineNumber, GroupIndex, ref TZs);
        }

        public bool GetGroupTZStr(int GroupIndex, ref string TZs)
        {
            return this.m_engine.GetGroupTZStr(this.m_engine.MachineNumber, GroupIndex, ref TZs);
        }

        public bool GetHIDEventCardNumAsStr(out string strHIDEventCardNum)
        {
            return this.m_engine.GetHIDEventCardNumAsStr(out strHIDEventCardNum);
        }

        public bool GetHoliday(ref string Holiday)
        {
            return this.m_engine.GetHoliday(this.m_engine.MachineNumber, ref Holiday);
        }

        public void GetLastError(ref int dwErrorCode)
        {
            this.m_engine.GetLastError(ref dwErrorCode);
        }

        public bool GetPIN2(int UserID, ref int PIN2)
        {
            return this.m_engine.GetPIN2(UserID, ref PIN2);
        }

        public bool GetPlatForm(ref string Platform)
        {
            return this.m_engine.GetPlatform(this.m_engine.MachineNumber, ref Platform);
        }

        public bool GetRTLog()
        {
            return this.m_engine.GetRTLog(this.m_engine.MachineNumber);
        }

        public bool GetProductCode(out string lpszProductCode)
        {
            return this.m_engine.GetProductCode(this.m_engine.MachineNumber, out lpszProductCode);
        }

        public bool GetSDKVersion(ref string strVersion)
        {
            return this.m_engine.GetSDKVersion(ref strVersion);
        }

        public bool GetSensorSN(ref string SensorSN)
        {
            return this.m_engine.GetSensorSN(this.m_engine.MachineNumber, ref SensorSN);
        }

        public bool GetSerialNumber(out string dwSerialNumber)
        {
            return this.m_engine.GetSerialNumber(this.m_engine.MachineNumber, out dwSerialNumber);
        }

        public bool GetSMS(
          int ID,
          ref int Tag,
          ref int ValidMinutes,
          ref string StartTime,
          ref string Content)
        {
            return this.m_engine.GetSMS(this.m_engine.MachineNumber, ID, ref Tag, ref ValidMinutes, ref StartTime, ref Content);
        }

        public string GetStrCardNumber()
        {
            string ACardNumber = "";
            return this.m_engine.GetStrCardNumber(out ACardNumber) ? ACardNumber : ACardNumber;
        }

        public bool GetSuperLogData(
          ref int dwTMachineNumber,
          ref int dwSEnrollNumber,
          ref int Params4,
          ref int Params1,
          ref int Params2,
          ref int dwManipulation,
          ref int Params3,
          ref int dwYear,
          ref int dwMonth,
          ref int dwDay,
          ref int dwHour,
          ref int dwMinute)
        {
            return this.m_engine.GetSuperLogData(this.m_engine.MachineNumber, ref dwTMachineNumber, ref dwSEnrollNumber, ref Params4, ref Params1, ref Params2, ref dwManipulation, ref Params3, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute);
        }

        public bool GetSysOption(string Option, out string Value)
        {
            return this.m_engine.GetSysOption(this.m_engine.MachineNumber, Option, out Value);
        }

        public bool GetTZInfo(int TZIndex, ref string TZ)
        {
            return this.m_engine.GetTZInfo(this.m_engine.MachineNumber, TZIndex, ref TZ);
        }

        public bool GetUnlockGroups(ref string Grps)
        {
            return this.m_engine.GetUnlockGroups(this.m_engine.MachineNumber, ref Grps);
        }

        public bool GetUserGroup(int dwEnrollNumber, ref int UserGrp)
        {
            return this.m_engine.GetUserGroup(this.m_engine.MachineNumber, dwEnrollNumber, ref UserGrp);
        }

        public bool GetUserIDByPIN2(int PIN2, ref int UserID)
        {
            return this.m_engine.GetUserIDByPIN2(PIN2, ref UserID);
        }

        public bool GetUserInfo(
          int dwEnrollNumber,
          ref string Name,
          ref string Password,
          ref int Privilege,
          ref bool Enabled)
        {
            return this.m_engine.GetUserInfo(this.m_engine.MachineNumber, dwEnrollNumber, ref Name, ref Password, ref Privilege, ref Enabled);
        }

        public bool GetUserInfoByCard(
          ref string Name,
          ref string Password,
          ref int Privilege,
          ref bool Enable)
        {
            return this.m_engine.GetUserInfoByCard(this.m_engine.MachineNumber, ref Name, ref Password, ref Privilege, ref Enable);
        }

        public bool GetUserInfoByPIN2(
          ref string Name,
          ref string Password,
          ref int Privilege,
          ref bool Enable)
        {
            return this.m_engine.GetUserInfoByPIN2(this.m_engine.MachineNumber, ref Name, ref Password, ref Privilege, ref Enable);
        }

        public bool GetUserInfoEx(int dwEnrollNumber, out int VerifyStyle, out byte Reserved)
        {
            return this.m_engine.GetUserInfoEx(this.m_engine.MachineNumber, dwEnrollNumber, out VerifyStyle, out Reserved);
        }

        public bool GetUserTmp(
          int dwEnrollNumber,
          int dwFingerIndex,
          ref byte TmpData,
          ref int TmpLength)
        {
            return this.m_engine.GetUserTmp(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, ref TmpData, ref TmpLength);
        }

        public bool GetUserTmpStr(
          int dwEnrollNumber,
          int dwFingerIndex,
          ref string TmpData,
          ref int TmpLength)
        {
            return this.m_engine.GetUserTmpStr(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, ref TmpData, ref TmpLength);
        }

        public bool GetUserTmpEx(
          string dwEnrollNumber,
          int dwFingerIndex,
          out int Flag,
          out byte TmpData,
          out int TmpLength)
        {
            return this.m_engine.GetUserTmpEx(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, out Flag, out TmpData, out TmpLength);
        }

        public bool GetUserTmpStrEx(
          string dwEnrollNumber,
          int dwFingerIndex,
          out int Flag,
          out string TmpData,
          out int TmpLength)
        {
            return this.m_engine.GetUserTmpExStr(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, out Flag, out TmpData, out TmpLength);
        }

        public bool GetUserTZs(int dwEnrollNumber, ref int TZs)
        {
            return this.m_engine.GetUserTZs(this.m_engine.MachineNumber, dwEnrollNumber, ref TZs);
        }

        public bool GetUserTZStr(int dwEnrollNumber, ref string TZs)
        {
            return this.m_engine.GetUserTZStr(this.m_engine.MachineNumber, dwEnrollNumber, ref TZs);
        }

        public bool GetVendor(ref string strVendor)
        {
            return this.m_engine.GetVendor(ref strVendor);
        }

        public bool GetWiegandFmt(ref string sWiegandFmt)
        {
            return this.m_engine.GetWiegandFmt(this.m_engine.MachineNumber, ref sWiegandFmt);
        }

        public bool GetWorkCode(int WorkCodeID, out int AWorkCode)
        {
            return this.m_engine.GetWorkCode(WorkCodeID, out AWorkCode);
        }

        public bool IsTFTMachine()
        {
            return this.m_engine.IsTFTMachine(this.m_engine.MachineNumber);
        }

        public int MachineNumber()
        {
            return this.m_engine.MachineNumber;
        }

        public bool ModifyPrivilege(
          int dwEnrollNumber,
          int dwEMachineNumber,
          int dwBackupNumber,
          int dwMachinePrivilege)
        {
            return this.m_engine.ModifyPrivilege(this.m_engine.MachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, dwMachinePrivilege);
        }

        public uint PIN2()
        {
            return this.m_engine.PIN2;
        }

        public int PINWidth()
        {
            return this.m_engine.PINWidth;
        }

        public bool PlayVoice(int Position, int Length)
        {
            return this.m_engine.PlayVoice(Position, Length);
        }

        public bool PlayVoiceByIndex(int Index)
        {
            return this.m_engine.PlayVoiceByIndex(Index);
        }

        public bool PowerOffDevice()
        {
            return this.m_engine.PowerOffDevice(this.m_engine.MachineNumber);
        }

        public void PowerOnAllDevice()
        {
            this.m_engine.PowerOnAllDevice();
        }

        public bool QueryState(ref int State)
        {
            return this.m_engine.QueryState(ref State);
        }

        public bool ReadAllGLogData()
        {
            return this.m_engine.ReadAllGLogData(this.m_engine.MachineNumber);
        }

        public bool ReadAllGlogDataEx(
          int dwStartYear,
          int dwStartMonth,
          int dwStartDay,
          int dwStartHour,
          int dwStartMinute,
          int dwStartSecond)
        {
            return this.m_engine.ReadAllGLogDataExt(this.m_engine.MachineNumber, dwStartYear, dwStartMonth, dwStartDay, dwStartHour, dwStartMinute, dwStartSecond);
        }

        public bool RealAllPhotoFileInfo(string FilePath, int flag)
        {
            return false;
        }

        public bool ReadAllSLogData()
        {
            return this.m_engine.ReadAllSLogData(this.m_engine.MachineNumber);
        }

        public bool ReadAllTemplate()
        {
            return this.m_engine.ReadAllTemplate(this.m_engine.MachineNumber);
        }

        public bool ReadAllUserID()
        {
            return this.m_engine.ReadAllUserID(this.m_engine.MachineNumber);
        }

        public bool ReadAOptions(string AOption, out string AValue)
        {
            return this.m_engine.ReadAOptions(AOption, out AValue);
        }

        public bool ReadAttRule()
        {
            return this.m_engine.ReadAttRule(this.m_engine.MachineNumber);
        }

        public bool ReadCustData(ref string CustData)
        {
            return this.m_engine.ReadCustData(this.m_engine.MachineNumber, ref CustData);
        }

        public bool ReadDPTInfo()
        {
            return this.m_engine.ReadDPTInfo(this.m_engine.MachineNumber);
        }

        public bool ReadFile(string FileName, string FilePath)
        {
            return this.m_engine.ReadFile(this.m_engine.MachineNumber, FileName, FilePath);
        }

        public bool ReadGeneralLogData()
        {
            return this.m_engine.ReadGeneralLogData(this.m_engine.MachineNumber);
        }

        public bool ReadMark()
        {
            return this.m_engine.ReadMark;
        }

        public bool ReadRTLog()
        {
            return this.m_engine.ReadRTLog(this.m_engine.MachineNumber);
        }

        public bool ReadSuperLogData()
        {
            return this.m_engine.ReadSuperLogData(this.m_engine.MachineNumber);
        }

        public bool ReadTimeGLogData(
          int sYear,
          int sMonth,
          int sDay,
          int sHour,
          int sMinute,
          int sSecond,
          int eYear,
          int eMonth,
          int eDay,
          int eHour,
          int eMinute,
          int eSecond)
        {
            if (this.m_engine.GetType().GetMethod(nameof(ReadTimeGLogData)) == null)
                throw new Exception("this zkeem DON'T support for ReadTimeGLogData function");
            return this.m_engine.ReadTimeGLogData(this.m_engine.MachineNumber, sYear, sMonth, sDay, sHour, sMinute, sSecond, eYear, eMonth, eDay, eHour, eMinute, eSecond);
        }

        public bool ReadTurnInfo()
        {
            return this.m_engine.ReadTurnInfo(this.m_engine.MachineNumber);
        }

        public bool RefreshData()
        {
            return this.IsConnected && this.m_engine.RefreshData(this.m_engine.MachineNumber);
        }

        public bool RegEvent(int EventMask)
        {
            return this.m_engine.RegEvent(this.m_engine.MachineNumber, EventMask);
        }

        public bool RestartDevice()
        {
            return this.m_engine.RestartDevice(this.m_engine.MachineNumber);
        }

        public bool RestoreData(string DataFile)
        {
            return this.m_engine.RestoreData(DataFile);
        }

        public bool SaveTheDataToFile(string TheFilePath, int FileFlag)
        {
            return this.m_engine.SaveTheDataToFile(this.m_engine.MachineNumber, TheFilePath, FileFlag);
        }

        public bool SendCMDMsg(int Param1, int Param2)
        {
            return this.m_engine.SendCMDMsg(this.m_engine.MachineNumber, Param1, Param2);
        }

        public void set_AccTimeZones(int Index, int pVal)
        {
            this.m_engine.set_AccTimeZones(Index, pVal);
        }

        public void set_CardNumber(int Index, int pVal)
        {
            this.m_engine.set_CardNumber(Index, pVal);
        }

        public void set_STR_CardNumber(int Index, string pVal)
        {
            this.m_engine.set_STR_CardNumber(Index, pVal);
        }

        public bool SetCommPassword(int CommKey)
        {
            return this.m_engine.SetCommPassword(CommKey);
        }

        public bool SetCustomizeAttState(int StateID, int NewState)
        {
            return this.m_engine.SetCustomizeAttState(this.m_engine.MachineNumber, StateID, NewState);
        }

        public bool SetCustomizeVoice(int VoiceID, string FileName)
        {
            return this.m_engine.SetCustomizeVoice(this.m_engine.MachineNumber, VoiceID, FileName);
        }

        public bool SetDeviceCommPwd(int CommKey)
        {
            return this.m_engine.SetDeviceCommPwd(this.m_engine.MachineNumber, CommKey);
        }

        public bool SetDeviceInfo(int dwInfo, int dwValue)
        {
            return this.m_engine.SetDeviceInfo(this.m_engine.MachineNumber, dwInfo, dwValue);
        }

        public bool SetDeviceIP(string IPAddr)
        {
            return this.m_engine.SetDeviceIP(this.m_engine.MachineNumber, IPAddr);
        }

        public bool SetDeviceMAC(string sMAC)
        {
            return this.m_engine.SetDeviceMAC(this.m_engine.MachineNumber, sMAC);
        }

        public bool SetDeviceTime()
        {
            return this.m_engine.SetDeviceTime(this.m_engine.MachineNumber);
        }

        public bool SetDeviceTime2(
          int dwYear,
          int dwMonth,
          int dwDay,
          int dwHour,
          int dwMinute,
          int dwSecond)
        {
            return this.m_engine.SetDeviceTime2(this.m_engine.MachineNumber, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond);
        }

        public bool SetEnrollData(
          int dwEnrollNumber,
          int dwEMachineNumber,
          int dwBackupNumber,
          int dwMachinePrivilege,
          ref int dwEnrollData,
          int dwPassWord)
        {
            return this.m_engine.SetEnrollData(this.m_engine.MachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, dwMachinePrivilege, ref dwEnrollData, dwPassWord);
        }

        public bool SetEnrollDataStr(
          int dwEnrollNumber,
          int dwEMachineNumber,
          int dwBackupNumber,
          int dwMachinePrivilege,
          string dwEnrollData,
          int dwPassWord)
        {
            return this.m_engine.SetEnrollDataStr(this.m_engine.MachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, dwMachinePrivilege, dwEnrollData, dwPassWord);
        }

        public bool SetGroupTZs(int GroupIndex, ref int TZs)
        {
            return this.m_engine.SetGroupTZs(this.m_engine.MachineNumber, GroupIndex, ref TZs);
        }

        public bool SetGroupTZStr(int GroupIndex, string TZs)
        {
            return this.m_engine.SetGroupTZStr(this.m_engine.MachineNumber, GroupIndex, TZs);
        }

        public bool SetHoliday(string Holiday)
        {
            return this.m_engine.SetHoliday(this.m_engine.MachineNumber, Holiday);
        }

        public bool SetLanguageByID(int LanguageID, string Language)
        {
            return this.m_engine.SetLanguageByID(this.m_engine.MachineNumber, LanguageID, Language);
        }

        public bool SetLastCount(int Count)
        {
            return this.m_engine.SetLastCount(Count);
        }

        public bool SetSMS(int ID, int Tag, int ValidMinutes, string StartTime, string Content)
        {
            return this.m_engine.SetSMS(this.m_engine.MachineNumber, ID, Tag, ValidMinutes, StartTime, Content);
        }

        public bool SetStrCardNumber(string strCardNumber)
        {
            return this.m_engine.SetStrCardNumber(strCardNumber);
        }

        public bool SetSysOption(string Option, string Value)
        {
            return this.m_engine.SetSysOption(this.m_engine.MachineNumber, Option, Value);
        }

        public bool SetTZInfo(int TZIndex, string TZ)
        {
            return this.m_engine.SetTZInfo(this.m_engine.MachineNumber, TZIndex, TZ);
        }

        public bool SetUnlockGroups(string Grps)
        {
            return this.m_engine.SetUnlockGroups(this.m_engine.MachineNumber, Grps);
        }

        public bool SetUserGroup(int dwEnrollNumber, int UserGrp)
        {
            return this.m_engine.SetUserGroup(this.m_engine.MachineNumber, dwEnrollNumber, UserGrp);
        }

        public bool SetUserInfo(
          int dwEnrollNumber,
          string Name,
          string Password,
          int Privilege,
          bool Enabled)
        {
            return this.m_engine.SetUserInfo(this.m_engine.MachineNumber, dwEnrollNumber, Name, Password, Privilege, Enabled);
        }

        public bool SetUserInfoEx(int dwEnrollNumber, int VerifyStyle, ref byte Reserved)
        {
            return this.m_engine.SetUserInfoEx(this.m_engine.MachineNumber, dwEnrollNumber, VerifyStyle, ref Reserved);
        }

        public bool SetUserSMS(int dwEnrollNumber, int SMSID)
        {
            return this.m_engine.SetUserSMS(this.m_engine.MachineNumber, dwEnrollNumber, SMSID);
        }

        public bool SetUserTmp(int dwEnrollNumber, int dwFingerIndex, ref byte TmpData)
        {
            return this.m_engine.SetUserTmp(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, ref TmpData);
        }

        public bool SetUserTmpStr(int dwEnrollNumber, int dwFingerIndex, string TmpData)
        {
            return this.m_engine.SetUserTmpStr(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, TmpData);
        }

        public bool SetUserTmpEx(string dwEnrollNumber, int dwFingerIndex, int Flag, ref byte TmpData)
        {
            return this.m_engine.SetUserTmpEx(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, Flag, ref TmpData);
        }

        public bool SetUserTmpStrEx(
          string dwEnrollNumber,
          int dwFingerIndex,
          int Flag,
          string TmpData)
        {
            return this.m_engine.SetUserTmpExStr(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, Flag, TmpData);
        }

        public bool SetUserTZs(int dwEnrollNumber, ref int TZs)
        {
            return this.m_engine.SetUserTZs(this.m_engine.MachineNumber, dwEnrollNumber, ref TZs);
        }

        public bool SetUserTZStr(int dwEnrollNumber, string TZs)
        {
            return this.m_engine.SetUserTZStr(this.m_engine.MachineNumber, dwEnrollNumber, TZs);
        }

        public bool SetWiegandFmt(string sWiegandFmt)
        {
            return this.m_engine.SetWiegandFmt(this.m_engine.MachineNumber, sWiegandFmt);
        }

        public bool SetWorkCode(int WorkCodeID, int AWorkCode)
        {
            return this.m_engine.SetWorkCode(WorkCodeID, AWorkCode);
        }

        public bool SSR_ClearWorkCode()
        {
            return this.m_engine.SSR_ClearWorkCode();
        }

        public bool SSR_DeleteEnrollData(string dwEnrollNumber, int dwBackupNumber)
        {
            return this.m_engine.SSR_DeleteEnrollData(this.m_engine.MachineNumber, dwEnrollNumber, dwBackupNumber);
        }

        public bool SSR_DeleteEnrollDataExt(string dwEnrollNumber, int dwBackupNumber)
        {
            return this.m_engine.SSR_DeleteEnrollDataExt(this.m_engine.MachineNumber, dwEnrollNumber, dwBackupNumber);
        }

        public bool SSR_DeleteUserSMS(string dwEnrollNumber, int SMSID)
        {
            return this.m_engine.SSR_DeleteUserSMS(this.m_engine.MachineNumber, dwEnrollNumber, SMSID);
        }

        public bool SSR_DelUserTmp(string dwEnrollNumber, int dwFingerIndex)
        {
            return this.m_engine.SSR_DelUserTmp(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex);
        }

        public bool SSR_DelUserTmpExt(string dwEnrollNumber, int dwFingerIndex)
        {
            return this.m_engine.SSR_DelUserTmpExt(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex);
        }

        public bool SSR_EnableUser(string dwEnrollNumber, bool bFlag)
        {
            return this.m_engine.SSR_EnableUser(this.m_engine.MachineNumber, dwEnrollNumber, bFlag);
        }

        public bool SSR_GetAllUserInfo(
          out string dwEnrollNumber,
          out string Name,
          out string Password,
          out int Privilege,
          out bool Enabled)
        {
            return this.m_engine.SSR_GetAllUserInfo(this.m_engine.MachineNumber, out dwEnrollNumber, out Name, out Password, out Privilege, out Enabled);
        }

        public bool SSR_GetGeneralLogData(
          out string dwEnrollNumber,
          out int dwVerifyMode,
          out int dwInOutMode,
          out int dwYear,
          out int dwMonth,
          out int dwDay,
          out int dwHour,
          out int dwMinute,
          out int dwSecond,
          ref int dwWorkCode)
        {
            return this.m_engine.SSR_GetGeneralLogData(this.m_engine.MachineNumber, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkCode);
        }

        public bool SSR_GetGroupTZ(
          int GroupNo,
          ref int Tz1,
          ref int Tz2,
          ref int Tz3,
          ref int ValidHoliday,
          ref int VerifyStyle)
        {
            return this.m_engine.SSR_GetGroupTZ(this.m_engine.MachineNumber, GroupNo, ref Tz1, ref Tz2, ref Tz3, ref ValidHoliday, ref VerifyStyle);
        }

        public bool GetHoliday(
          int HolidayID,
          ref int BeginMonth,
          ref int BeginDay,
          ref int EndMonth,
          ref int EndDay,
          ref int TimeZoneID)
        {
            return this.m_engine.SSR_GetHoliday(this.m_engine.MachineNumber, HolidayID, ref BeginMonth, ref BeginDay, ref EndMonth, ref EndDay, ref TimeZoneID);
        }

        public bool SSR_GetShortkey(
          int ShortKeyID,
          ref int ShortKeyFun,
          ref int StateCode,
          ref string StateName,
          ref int AutoChange,
          ref string AutoChangeTime)
        {
            return this.m_engine.SSR_GetShortkey(ShortKeyID, ref ShortKeyFun, ref StateCode, ref StateName, ref AutoChange, ref AutoChangeTime);
        }

        public bool SSR_GetSuperLogData(
          out int Number,
          out string Admin,
          out string User,
          out int Manipulation,
          out string Timeout,
          out int Params1,
          out int Params2,
          out int Params3)
        {
            return this.m_engine.SSR_GetSuperLogData(this.m_engine.MachineNumber, out Number, out Admin, out User, out Manipulation, out Timeout, out Params1, out Params2, out Params3);
        }

        public bool SSR_GetUnLockGroup(
          int Combo,
          ref int Group1,
          ref int Group2,
          ref int Group3,
          ref int Group4,
          ref int Group5)
        {
            return this.m_engine.SSR_GetUnLockGroup(this.m_engine.MachineNumber, Combo, ref Group1, ref Group2, ref Group3, ref Group4, ref Group5);
        }

        public bool SSR_GetUserGroup(string dwEnrollNumber, ref int UserGrp)
        {
            return false;
        }

        public bool SSR_GetUserInfo(
          string dwEnrollNumber,
          out string Name,
          out string Password,
          out int Privilege,
          out bool Enabled)
        {
            return this.m_engine.SSR_GetUserInfo(this.m_engine.MachineNumber, dwEnrollNumber, out Name, out Password, out Privilege, out Enabled);
        }

        public bool SSR_GetUserTmp(
          string dwEnrollNumber,
          int dwFingerIndex,
          out byte TmpData,
          out int TmpLength)
        {
            return this.m_engine.SSR_GetUserTmp(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, out TmpData, out TmpLength);
        }

        public bool SSR_GetUserTmpStr(
          string dwEnrollNumber,
          int dwFingerIndex,
          out string TmpData,
          out int TmpLength)
        {
            return this.m_engine.SSR_GetUserTmpStr(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, out TmpData, out TmpLength);
        }

        public bool SSR_GetUserStr(string dwEnrollNumber, ref int Tzs)
        {
            return false;
        }

        public bool SSR_GetWorkCode(int AWorkCode, out string Name)
        {
            return this.m_engine.SSR_GetWorkCode(AWorkCode, out Name);
        }

        public bool SSR_OutPutHTMLRep(
          string dwEnrollNumber,
          string AttFile,
          string UserFile,
          string DeptFile,
          string TimeClassFile,
          string AttruleFile,
          int BYear,
          int BMonth,
          int BDay,
          int BHour,
          int BMinute,
          int BSecond,
          int EYear,
          int EMonth,
          int EDay,
          int EHour,
          int EMinute,
          int ESecond,
          string TempPath,
          string OutFileName,
          int HTMLFlag,
          int resv1,
          string resv2)
        {
            return this.m_engine.SSR_OutPutHTMLRep(this.m_engine.MachineNumber, dwEnrollNumber, AttFile, UserFile, DeptFile, TimeClassFile, AttruleFile, BYear, BMonth, BDay, BHour, BMinute, BSecond, EYear, EMonth, EDay, EHour, EMinute, ESecond, TempPath, OutFileName, HTMLFlag, resv1, resv2);
        }

        public bool SSR_SetGroupTz(
          int GrounpNo,
          int Tz1,
          int Tz2,
          int Tz3,
          int ValidHoliday,
          int VerifyStyle)
        {
            return this.m_engine.SSR_SetGroupTZ(this.m_engine.MachineNumber, GrounpNo, Tz1, Tz2, Tz3, ValidHoliday, VerifyStyle);
        }

        public bool SSR_SetHoliday(
          int HolidayID,
          int BeginMonth,
          int BeginDay,
          int EndMonth,
          int EndDay,
          int TimezineID)
        {
            return this.m_engine.SSR_SetHoliday(this.m_engine.MachineNumber, HolidayID, BeginMonth, BeginDay, EndMonth, EndDay, TimezineID);
        }

        public bool SSR_SetShortkey(
          int ShortKeyID,
          int ShortKeyFun,
          int StateCode,
          string StateName,
          int StateAutoChange,
          string StateAutoChangeTime)
        {
            return this.m_engine.SSR_SetShortkey(ShortKeyID, ShortKeyFun, StateCode, StateName, StateAutoChange, StateAutoChangeTime);
        }

        public bool SSR_SetUnLockGroup(
          int ComboNo,
          int Group1,
          int Group2,
          int Group3,
          int Group4,
          int Group5)
        {
            return this.m_engine.SSR_SetUnLockGroup(this.m_engine.MachineNumber, ComboNo, Group1, Group2, Group3, Group4, Group5);
        }

        public bool SSR_SetUserGroup(string dwEnrollNumber, int UserGrp)
        {
            return false;
        }

        public bool SSR_SetUserInfo(
          string dwEnrollNumber,
          string Name,
          string Password,
          int Privilege,
          bool Enabled)
        {
            return this.m_engine.SSR_SetUserInfo(this.m_engine.MachineNumber, dwEnrollNumber, Name, Password, Privilege, Enabled);
        }

        public bool SSR_SetUserSMS(string dwEnrollNumber, int SMSID)
        {
            return this.m_engine.SSR_SetUserSMS(this.m_engine.MachineNumber, dwEnrollNumber, SMSID);
        }

        public bool SSR_SetUserTmp(string dwEnrollNumber, int dwFingerIndex, ref byte TmpData)
        {
            return this.m_engine.SSR_SetUserTmp(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, ref TmpData);
        }

        public bool SSR_SetUserTmpExt(
          int IsDeleted,
          string dwEnrollNumber,
          int dwFingerIndex,
          ref byte TempData)
        {
            return this.m_engine.SSR_SetUserTmpExt(this.m_engine.MachineNumber, IsDeleted, dwEnrollNumber, dwFingerIndex, ref TempData);
        }

        public bool SSR_SetUserTmpStr(string dwEnrollNumber, int dwFingerIndex, string TmpData)
        {
            return this.m_engine.SSR_SetUserTmpStr(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex, TmpData);
        }

        public bool SSR_SetUserTZs(string dwEnrollNumber, ref int TZs)
        {
            return false;
        }

        public bool SSR_SetWorkCode(int AWorkCode, string Name)
        {
            return this.m_engine.SSR_SetWorkCode(AWorkCode, Name);
        }

        public int SSRPin()
        {
            return this.m_engine.SSRPin;
        }

        public bool StartEnroll(int UserID, int FingerID)
        {
            return this.m_engine.StartEnroll(UserID, FingerID);
        }

        public bool StartEnrollExt(string UserID, int FingerID, int Flag)
        {
            return this.m_engine.StartEnrollEx(UserID, FingerID, Flag);
        }

        public bool StartIdentify()
        {
            return this.m_engine.StartIdentify();
        }

        public bool StartVerify(int UserID, int FingerID)
        {
            return this.m_engine.StartVerify(UserID, FingerID);
        }

        public bool UpdateFirmware(string FirmwareFile)
        {
            return this.m_engine.UpdateFirmware(FirmwareFile);
        }

        public bool UploadUserPhoto(string pFileName)
        {
            return this.m_engine.UploadUserPhoto(this.m_engine.MachineNumber, pFileName);
        }

        public bool UseGroupTimeZone()
        {
            return this.m_engine.UseGroupTimeZone();
        }

        public bool WriteCard(
          int dwEnrollNumber,
          int dwFingerIndex1,
          ref byte TmpData1,
          int dwFingerIndex2,
          ref byte TmpData2,
          int dwFingerIndex3,
          ref byte TmpData3,
          int dwFingerIndex4,
          ref byte TmpData4)
        {
            return this.m_engine.WriteCard(this.m_engine.MachineNumber, dwEnrollNumber, dwFingerIndex1, ref TmpData1, dwFingerIndex2, ref TmpData2, dwFingerIndex3, ref TmpData3, dwFingerIndex4, ref TmpData4);
        }

        public bool WriteCustData(string CustData)
        {
            return this.m_engine.WriteCustData(this.m_engine.MachineNumber, CustData);
        }

        public bool WriteLCD(string content)
        {
            return this.m_engine.WriteLCD(1, 0, content);
        }

        public int GetCardNumber(int userID)
        {
            string Name = "";
            string Password = "";
            int Privilege = 0;
            bool Enabled = true;
            this.m_engine.GetUserInfo(1, userID, ref Name, ref Password, ref Privilege, ref Enabled);
            return this.m_engine.get_CardNumber(0);
        }

        public bool SetNewUserInfo(
          int userID,
          int cardNumber,
          string nameOnMachine,
          int privilege,
          bool enable,
          string strTimezone)
        {
            this.m_engine.set_CardNumber(0, cardNumber);
            bool flag = this.m_engine.SetUserInfo(this.m_engine.MachineNumber, userID, nameOnMachine, "", privilege, enable);
            if (flag)
                flag = this.m_engine.SetUserTZStr(this.m_engine.MachineNumber, userID, strTimezone);
            return flag;
        }

        public bool UpdateUserInfo(int userID, int privilege, bool enable, string strTimezone)
        {
            bool flag = this.m_engine.EnableUser(this.m_engine.MachineNumber, userID, 0, 0, enable);
            if (flag)
                flag = this.m_engine.ModifyPrivilege(this.m_engine.MachineNumber, userID, 0, 0, privilege);
            if (flag)
                flag = this.m_engine.SetUserTZStr(this.m_engine.MachineNumber, userID, strTimezone);
            return flag;
        }

        public bool GetUserInfoByCard(
          int cardNumber,
          ref string Name,
          ref string Password,
          ref int Privilege,
          ref bool Enabled)
        {
            this.m_engine.set_CardNumber(0, cardNumber);
            return this.m_engine.GetUserInfoByCard(this.m_engine.MachineNumber, ref Name, ref Password, ref Privilege, ref Enabled);
        }

        public bool GetUserInfoByPIN2(
          int PIN2,
          ref string Name,
          ref string Password,
          ref int Privilege,
          ref bool Enabled)
        {
            int UserID = 0;
            this.m_engine.GetUserIDByPIN2(PIN2, ref UserID);
            return this.m_engine.GetUserInfo(this.m_engine.MachineNumber, UserID, ref Name, ref Password, ref Privilege, ref Enabled);
        }

        private void m_engine_OnDoor(int d)
        {
            Trace.WriteLine("OnDoor " + (object)d);
        }

        private void m_engine_OnFingerFeature(int d)
        {
            Trace.WriteLine("OnFingerFeature " + (object)d);
        }

        private void m_engine_OnFinger()
        {
        }

        private void m_engine_OnVerify(int userID)
        {
            this.m_VerifyMode = 1;
            if (this.OnVerifyEventHandler == null)
                return;
            this.OnVerifyEventHandler(userID, this.m_IP);
        }

        private void m_engine_OnHIDNum(int cardNumber)
        {
            this.m_VerifyMode = 2;
            if (this.OnHIDNumEventHandler == null)
                return;
            this.OnHIDNumEventHandler(cardNumber, this.m_IP);
        }

        private void m_engine_OnAttTransactionEx(
          string EnrollNumber,
          int IsInValid,
          int AttState,
          int VerifyMethod,
          int Year,
          int Month,
          int Day,
          int Hour,
          int Minute,
          int Second,
          int WorkCode)
        {
            if (VerifyMethod != 0)
                this.m_VerifyMode = VerifyMethod;
            if (this.OnAttTransactionExEventHandler != null)
                this.OnAttTransactionExEventHandler(int.Parse(EnrollNumber), this.m_IP, IsInValid, AttState, this.m_VerifyMode, Year, Month, Day, Hour, Minute, Second);
            this.m_VerifyMode = 0;
        }

        protected void CreateEngines()
        {
            this.m_engine = new CZKEM();
            //// ISSUE: method pointer
            //this.m_engine.add_OnFinger(new _IZKEMEvents_OnFingerEventHandler((object)this, (UIntPtr)__methodptr(m_engine_OnFinger)));
            //// ISSUE: method pointer
            //this.m_engine.add_OnFingerFeature(new _IZKEMEvents_OnFingerFeatureEventHandler((object)this, (UIntPtr)__methodptr(m_engine_OnFingerFeature)));
            //// ISSUE: method pointer
            //this.m_engine.add_OnDoor(new _IZKEMEvents_OnDoorEventHandler((object)this, (UIntPtr)__methodptr(m_engine_OnDoor)));
            //// ISSUE: method pointer
            //this.m_engine.add_OnVerify(new _IZKEMEvents_OnVerifyEventHandler((object)this, (UIntPtr)__methodptr(m_engine_OnVerify)));
            //// ISSUE: method pointer
            //this.m_engine.add_OnHIDNum(new _IZKEMEvents_OnHIDNumEventHandler((object)this, (UIntPtr)__methodptr(m_engine_OnHIDNum)));
            //// ISSUE: method pointer
            //this.m_engine.add_OnAttTransactionEx(new _IZKEMEvents_OnAttTransactionExEventHandler((object)this, (UIntPtr)__methodptr(m_engine_OnAttTransactionEx)));
        }
    }
}
