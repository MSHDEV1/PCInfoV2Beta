using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;


using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace pcinformation
{
  
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
        ManagementObjectSearcher motherboardv2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
        ManagementObjectSearcher motherboard = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");
        ManagementObjectSearcher islem = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
        ManagementObjectSearcher islem2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
        ManagementObjectSearcher monitor = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DesktopMonitor ");
        ManagementObjectSearcher bios = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
        ManagementObjectSearcher grapich = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
        ManagementObjectSearcher disk = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
        PerformanceCounter perf = new PerformanceCounter("System","System Up Time");
       
      

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button1.BackColor = Color.LightGray;
            timer1.Stop();
            
            timer2.Stop();
            listBox1.Items.Clear();
            string pcname = SystemInformation.ComputerName.ToString();
            string version = Environment.OSVersion.ToString();
            listBox1.Items.Add("Bilgisayar İsmi:"+" "+pcname);
         
            listBox1.Items.Add("İşletim Sistemi Versionu:"+" "+version);
            
            foreach (ManagementObject isletim in islem2.Get())
            {
                listBox1.Items.Add("İşletim Sistemi:" + " " + isletim["Caption"].ToString());
               
                listBox1.Items.Add("İşletim Sistemi Bit:"+" "+ isletim["OSArchitecture"].ToString());
                
            }
            string directory = Environment.SystemDirectory;
            listBox1.Items.Add("Sistem Konumu:"+" "+ directory);
            foreach (ManagementObject bios1 in bios.Get()) {
                string biosversion = bios1["Description"].ToString();
                listBox1.Items.Add("BIOS Versionu:" + " " + biosversion);
            }
            foreach (ManagementObject monitor1 in monitor.Get())
            {
                string monitor = SystemInformation.PrimaryMonitorSize.ToString();
                string monitor2 = SystemInformation.MonitorCount.ToString();
                listBox1.Items.Add("Monitör En Boy Oranı:" + " " + monitor);
                listBox1.Items.Add("Monitör Adeti:" + " " + monitor2);
            }
            foreach (ManagementObject grapich in grapich.Get())
            {
                listBox1.Items.Add("Monitör Maximum Yenileme Hızı:" + " " + grapich["MaxRefreshRate"].ToString()+"Hz");
                listBox1.Items.Add("Monitör Minimum Yenileme Hızı:" + " " + grapich["MinRefreshRate"].ToString() + "Hz");
            }
            foreach(ManagementObject disk in disk.Get())
            {
                double diskdriversize =Convert.ToDouble(disk["Size"]) / (1024.0*1024.0*1024.0);
                listBox1.Items.Add("Disk Boyutu:"+" " + diskdriversize.ToString("0.00")+"GB");
            }
            
        }


        private void Form2_Load(object sender, EventArgs e)
        {

            button1.BackColor = Color.LightGray;

            string pcname = SystemInformation.ComputerName.ToString();
            string version = Environment.OSVersion.ToString();
            listBox1.Items.Add("Bilgisayar İsmi:" + " " + pcname);

            listBox1.Items.Add("İşletim Sistemi Versionu:" + " " + version);

            foreach (ManagementObject isletim in islem2.Get())
            {
                listBox1.Items.Add("İşletim Sistemi:" + " " + isletim["Caption"].ToString());

                listBox1.Items.Add("İşletim Sistemi Bit:" + " " + isletim["OSArchitecture"].ToString());

            }
            string directory = Environment.SystemDirectory;
            listBox1.Items.Add("Sistem Konumu:" + " " + directory);
            foreach (ManagementObject bios1 in bios.Get())
            {
                string biosversion = bios1["Description"].ToString();
                listBox1.Items.Add("BIOS Versionu:" + " " + biosversion);
            }
            foreach (ManagementObject monitor1 in monitor.Get())
            {
                string monitor = SystemInformation.PrimaryMonitorSize.ToString();
                string monitor2 = SystemInformation.MonitorCount.ToString();
                listBox1.Items.Add("Monitör En Boy Oranı:" + " " + monitor);
                listBox1.Items.Add("Monitör Adeti:" + " " + monitor2);
            }
            foreach(ManagementObject grapich in grapich.Get())
            {
                listBox1.Items.Add("Monitör Maximum Yenileme Hızı:" + "" + grapich["MaxRefreshRate"].ToString() + "Hz");
                listBox1.Items.Add("Monitör Minimum Yenileme Hızı:" + "" + grapich["MinRefreshRate"].ToString() + "Hz");
            }
            foreach (ManagementObject disk in disk.Get())
            {
                double diskdriversize = Convert.ToDouble(disk["Size"]) / (1024.0 * 1024.0 * 1024.0);
                listBox1.Items.Add("Disk Boyutu:" + " " + diskdriversize.ToString("0.00") + "GB");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (ManagementObject cpu in mos.Get())
                {
                    listBox1.Items[2] = ("İşlemci Kullanımı" + " " + cpu["LoadPercentage"].ToString());


                }
            }
            catch
            {
                foreach (ManagementObject cpu in mos.Get())
                {
                    listBox1.Items[2] = ("İşlemci Kullanımı" + " " + "CPU'ya erişilemiyor");

                   
                }
            }
          
                foreach (ManagementObject islem in islem2.Get())
                {
                    listBox1.Items[3] = ("İşlemci İşlem Sayısı:" + " " + islem["NumberOfProcesses"].ToString());
                }
            double time = perf.NextValue() / 60 / 60;
            listBox1.Items[5] = ("PC Açık Kalma Süresi:"+" "+ time.ToString("0")+" "+"Saat");
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.White;
            button1.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button3.BackColor = Color.LightGray;
            timer1.Stop();
            timer2.Start();
            listBox1.Items.Clear();
            foreach (ManagementObject ram in islem2.Get())
            {
                double ramtotal =Convert.ToDouble(ram["TotalVisibleMemorySize"]) / (1024.0 * 1024.0);
                listBox1.Items.Add("Toplam Ram Miktarı:" + " "+ ramtotal.ToString("0.00")+"GB");
                listBox1.Items.Add("Kullanılan Ram Miktarı:" + " " );
                listBox1.Items.Add("Boştaki Ram Miktarı:" + " ");
                
            }
            foreach(ManagementObject ram in islem.Get())
            {
                listBox1.Items.Add("Ram Frekans Hızı(MHZ)" + " " + ram["Speed"].ToString() + "MHz");
               
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach(ManagementObject ram in islem2.Get())
            {
                double total = Convert.ToDouble(ram["TotalVisibleMemorySize"]) / (1024.0 * 1024.0);
                double totalv2 = Convert.ToDouble(ram["FreePhysicalMemory"]) / (1024.0 * 1024.0);
                double ram1 = total - totalv2;
                listBox1.Items[1] = ("Kullanılan Ram Miktarı:" + " "+ ram1.ToString("0.00")+"GB");
                double freeram = Convert.ToDouble(ram["FreePhysicalMemory"]) / (1024.0 * 1024.0);
                listBox1.Items[2] = ("Boştaki Ram Miktarı:" + " "+freeram.ToString("0.00")+"GB");
                
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
            button2.BackColor = Color.LightGray;
            listBox1.Items.Clear();
            timer2.Stop();
            timer1.Start();
            foreach (ManagementObject cpu in mos.Get())
            {
                listBox1.Items.Add("İşlemci Modeli:" + "" + cpu["Name"].ToString());
                listBox1.Items.Add("İşlemci Çekirdek Sayısı:" + "" + cpu["ThreadCount"].ToString());
                listBox1.Items.Add("İşlemci Kullanımı");
                listBox1.Items.Add("İşlemci İşlem Sayısı:");
                

            }
            
            foreach(ManagementObject grapich in grapich.Get())
            {
                listBox1.Items.Add("Ekran Kartı Modeli:" + "" + grapich["Name"].ToString());
                Int64 grapichmemory = Convert.ToInt64(grapich["AdapterRAM"]) / (1024 * 1024);
                Console.WriteLine(grapich["AdapterRAM"]);
               


            }
            
            listBox1.Items.Add("PC Açık Kalma Süresi:");

        }
     
        private void button4_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button1.BackColor = Color.White;
            button5.BackColor = Color.White;
            button4.BackColor = Color.LightGray;
            timer1.Stop();
            timer2.Stop();
            listBox1.Items.Clear();
            try
            {
                string ip = Dns.GetHostName();
                listBox1.Items.Add("Bilgisayar İsmi:" + " " + ip);
            }
            catch
            {
                listBox1.Items.Add("Bilgisayar İsmi:" + " " + "");
            }
            try
            {
                string ip = Dns.GetHostName();
                for (int i = 0;i<= 10; i++)
                {
                    if (Dns.GetHostByName(ip).AddressList[i] != null)
                    {
                        var ipadres = Dns.GetHostByName(ip).AddressList[i].ToString();
                        int num = i + 1;
                        listBox1.Items.Add("İp Adresi"+num+":"+ " " + ipadres);
                       
                    }
  
                }      
            }
            catch
            {
               
            }
            try
            {
                IPAddress ipAddresslocal = Dns.Resolve("localhost").AddressList[0];
                listBox1.Items.Add("İp Adresi(LocalHost):" + " " + ipAddresslocal);
            }
            catch
            {
                listBox1.Items.Add("İp Adresi(LocalHost):" + " " + "İp Alınamadı");
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button1.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.LightGray;
            listBox1.Items.Clear();
            timer1.Stop();
            timer2.Stop();
            foreach(ManagementObject bios in bios.Get())
            {
                listBox1.Items.Add("Anakart Yazılım Yayın:" + " " + bios["Manufacturer"]);
            }
            foreach (ManagementObject mother2 in motherboardv2.Get())
            {
                listBox1.Items.Add("Anakart Yapımcısı:" + " " + mother2["Manufacturer"]);
                listBox1.Items.Add("Anakart Modeli:" + " " + mother2["Product"]);
                

            }
            foreach(ManagementObject bios in bios.Get())
            {
                listBox1.Items.Add("Anakart Versionu:" + " " + bios["Description"]);
               
            }
            foreach (ManagementObject mother in motherboard.Get())
            {
                listBox1.Items.Add("Anakart BUS Tipi:" + " " + mother["PrimaryBusType"]);
              
            }
        }

    }
}
