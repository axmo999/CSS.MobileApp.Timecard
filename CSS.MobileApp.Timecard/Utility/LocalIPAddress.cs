using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace CSS.MobileApp.Timecard.Utility
{
    public class LocalIPAddress
    {
        public List<IPAddress> GetLocalIPAddress()
        {
            var ipaddress = new List<IPAddress>();

            // 物理インターフェース情報をすべて取得
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            // 各インターフェースごとの情報を調べる
            foreach (var adapter in interfaces)
            {
                // 有効なインターフェースのみを対象とする
                if (adapter.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }

                // インターフェースに設定されたIPアドレス情報を取得
                var properties = adapter.GetIPProperties();

                // 設定されているすべてのユニキャストアドレスについて
                foreach (var unicast in properties.UnicastAddresses)
                {
                    if (unicast.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        // IPv4アドレス
                        ipaddress.Add(unicast.Address);
                    }
                    //else if (unicast.Address.AddressFamily == AddressFamily.InterNetworkV6)
                    //{
                    //    // IPv6アドレス
                    //    ipaddress.Add(unicast.Address);
                    //}
                }
            }

            return ipaddress;
        }
    }
}