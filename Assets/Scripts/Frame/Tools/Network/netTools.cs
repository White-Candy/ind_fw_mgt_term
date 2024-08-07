
using System.Net.NetworkInformation;

public static class netTools
{
    /// <summary>
    /// 获取主机的ipv4地址
    /// </summary>
    /// <returns></returns>
    public static string GetIPForTypeIPV4()
    {
        string ipv4_ip = "127.0.0.1";
        foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
        {
            NetworkInterfaceType wirless = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType Ethernet = NetworkInterfaceType.Ethernet;
            NetworkInterfaceType item_networkType = item.NetworkInterfaceType;
            if ((item_networkType == wirless || item_networkType == Ethernet) && item.OperationalStatus == OperationalStatus.Up)
            {
                foreach (var ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipv4_ip = ip.Address.ToString();
                    }
                }
            }
        }
        return ipv4_ip;
    }
}