using System.Runtime.InteropServices;
using System.Text;
using Raylib_CsLo;
using Serilog;

namespace Engine3D.Extras;

public static unsafe class CustomLogging
{

    [DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern int vsprintf(
        StringBuilder buffer,
        string format,
        IntPtr args);

    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int _vscprintf(
        string format,
        IntPtr ptr);


    // Custom logging funtion
    //private static void LogCustom(int msgType, char* text, __arglist) //va_list args
    [UnmanagedCallersOnly(CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
    public static void LogCustom(int msgType, sbyte* text, sbyte* args)
    {
        //Console.WriteLine("hi");

        //char timeStr[64] = { 0 };
        //time_t now = time(NULL);
        //struct tm *tm_info = localtime(&now);
        //strftime(timeStr, sizeof(timeStr), "%Y-%m-%d %H:%M:%S", tm_info);
        //printf("[%s] ", timeStr);
        var textStr = Marshal.PtrToStringUTF8((IntPtr) text) ?? "";
        
        var sb = new StringBuilder(_vscprintf(textStr, (IntPtr) args) + 1);
        vsprintf(sb, textStr, (IntPtr) args);

        //here formattedMessage has the value your are looking for
        var formattedMessage = sb.ToString();
        
        switch ((TraceLogLevel) msgType)
        {
            case TraceLogLevel.LOG_INFO:
                /*Console.Write($"[INFO] {msgType} :");*/
                Log.Logger.Information(formattedMessage);
                break;
            case TraceLogLevel.LOG_ERROR:
                /*Console.Write($"[ERROR] {msgType} :");*/
                Log.Logger.Error(formattedMessage);
                break;
            case TraceLogLevel.LOG_WARNING:
                /*Console.Write($"[WARN] {msgType} :");*/
                Log.Logger.Warning(formattedMessage);
                break;
            case TraceLogLevel.LOG_DEBUG:
                /*Console.Write($"[DEBUG] {msgType} :");*/
                Log.Logger.Debug(formattedMessage);
                break;
            default:
                /*Console.Write($"[???] {msgType} :");*/
                break;
        }
    }
}