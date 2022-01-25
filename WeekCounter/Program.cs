// https://superuser.com/questions/1534488/how-can-i-show-weeknumber-in-the-taskbar-in-windows-10
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;

namespace WeekCounter // https://superuser.com/questions/1534488/how-can-i-show-weeknumber-in-the-taskbar-in-windows-10
{
    static class Program // https://superuser.com/questions/1534488/how-can-i-show-weeknumber-in-the-taskbar-in-windows-10
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyCustomApplicationContext());
        }
        private static void MessageBox(string v)
        {
            throw new NotImplementedException();
        }
    }
    public class MyCustomApplicationContext : ApplicationContext // https://superuser.com/questions/1534488/how-can-i-show-weeknumber-in-the-taskbar-in-windows-10
    {
        private NotifyIcon trayIcon;
        private Icon trayIconWeekNr()
        {
            // CultureInfo myCI = new CultureInfo("en-US");
            CultureInfo myCI = new CultureInfo("sv-SE");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            // trayIcon.Text = "Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt") + "\nWeek: " + myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);

            // https://stackoverflow.com/questions/36379547/writing-text-to-the-system-tray-instead-of-an-icon
            Font fontToUse = new Font("Microsoft Sans Serif", 16, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brushToUse = new SolidBrush(Color.White);
            Bitmap bitmapText = new Bitmap(16, 16);
            Graphics g = System.Drawing.Graphics.FromImage(bitmapText);
            IntPtr hIcon;
            g.Clear(Color.Transparent);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            string week = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW).ToString();
            g.DrawString(week, fontToUse, brushToUse, -4, -2);
            hIcon = (bitmapText.GetHicon());
            return System.Drawing.Icon.FromHandle(hIcon);
        }
        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = trayIconWeekNr(), // WeekCounter.Properties.Resources.icon,
                Text = "WeekCounter",
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Exit", Exit)
                }),
                Visible = true
            };
            trayIcon.MouseMove += new MouseEventHandler(notifyIcon1_MouseMove);
        }
        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e) // https://superuser.com/questions/1534488/how-can-i-show-weeknumber-in-the-taskbar-in-windows-10
        {
            // CultureInfo myCI = new CultureInfo("en-US");
            CultureInfo myCI = new CultureInfo("sv-SE");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            trayIcon.Text = "Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt") + "\nWeek: " + myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);
            trayIcon.Icon = trayIconWeekNr();
        }
        void Exit(object sender, EventArgs e) // https://superuser.com/questions/1534488/how-can-i-show-weeknumber-in-the-taskbar-in-windows-10
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            Application.Exit();
        }
    }
}