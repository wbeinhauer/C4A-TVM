using FirstFloor.ModernUI.Windows;
using Microsoft.PointOfService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using FirstFloor.ModernUI.Windows.Navigation;


namespace SampleApp.Pages
{
    /// <summary>
    /// Interaction logic for Print.xaml
    /// </summary>
    public partial class Print : UserControl, IContent
    {
        bool isLeavePageAllowed;

        public Print()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (isLeavePageAllowed == false || e.Source.OriginalString != "/Pages/Home.xaml")
            {
                e.Cancel = true;
                return;
            }
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += backgroundWorker_LightsOff;
            //backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_LightsOff(object sender, DoWorkEventArgs e)
        {
            PosExplorer explorer = new PosExplorer();
            PosCommon doorhub = null;

            var commonCollection = explorer.GetDevices("PosCommon");
            foreach (DeviceInfo deviceInfo in commonCollection)
            {
                if (deviceInfo.ServiceObjectName != "DoorHub")
                    continue;
                doorhub = (PosCommon)explorer.CreateInstance(deviceInfo);
                break;
            }
            if (doorhub == null)
            {
                MessageBox.Show("Doorhub not activated");
                return;
            }

            doorhub.Open();
            doorhub.Claim(1000);
            doorhub.DeviceEnabled = true;

            DirectIOData data = doorhub.DirectIO(1, 0, new string[] { "HexMaskGPB", "00" });

            doorhub.DeviceEnabled = false;
            doorhub.Release();
            doorhub.Close();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            isLeavePageAllowed = false;

            Head.Text = "Printing Ticket...";
            Subtitle.Text = "Please wait...";

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_Print;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void backgroundWorker_Print(object sender, DoWorkEventArgs e)
        {

            PosPrinter printer = null;
           

            string doneHead = "Finished Printing.";
            string waitMsg = "Please wait...";
            string doneSingleMsg = "Please take your ticket.";
            string doneMulMsg = "Please take your tickets.";

            int ticketsToPrint = 1;
            

            int price = Session.Instance.Price;
            string ticket = Session.Instance.Ticket;
            string testImageFilename = "";

            string TicketHeader = "";

            switch (Session.Instance.Ticket)
            {
                case "Whole Day Ticket":
                    break;
            }
            
            if (Session.Instance.Ticket == "Whole Day Ticket")
            {
                TicketHeader = "Whole Day Ticket";
                ticketsToPrint = 1;
                testImageFilename = System.IO.Path.GetFullPath(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\Demo Tickets\\wholeDay.bmp");
            }
            else if (Session.Instance.Ticket == "Ten Tickets")
            {
                TicketHeader = "Single Ticket";
                ticketsToPrint = 10;
                testImageFilename = System.IO.Path.GetFullPath(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\Demo Tickets\\singleTicket.bmp");
            }
            else if (Session.Instance.Ticket == "Five Tickets")
            {
                TicketHeader = "Single Ticket";
                ticketsToPrint = 5;
                testImageFilename = System.IO.Path.GetFullPath(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\Demo Tickets\\singleTicket.bmp");
            }
            else if (Session.Instance.Ticket == "Single Ticket")
            {
                TicketHeader = "Single Ticket";
                ticketsToPrint = 1;
                testImageFilename = System.IO.Path.GetFullPath(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\Demo Tickets\\singleTicket.bmp");
            }

            float singleTicketPrice = ((float)Session.Instance.Price) / ticketsToPrint / 100;
            string priceString = String.Format("EUR: {0:0.00}", singleTicketPrice);

            if (Session.Instance.CashChanger != null)
            {
                Session.Instance.CashChanger.Close();
                Session.Instance.CashChanger = null;
            }
            printer = Session.Instance.PosPrinter;
            if (printer == null)
            {
                MessageBox.Show("Printer not activated");
                return;
            }

            Dispatcher.BeginInvoke(new Action(() =>
            {
                Subtitle.Text = waitMsg;
            }));

            for (int i = 0; i < ticketsToPrint; i++)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Head.Text = String.Format("Printing Ticket {0} of {1}...", i + 1, ticketsToPrint);
                }));

                Bitmap createdBitmap = new System.Drawing.Bitmap(960, 640, PixelFormat.Format24bppRgb);
                RectangleF rectf = new RectangleF(0, 0, createdBitmap.Width, createdBitmap.Height);

                Graphics g = Graphics.FromImage(createdBitmap);

                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.FillRectangle(System.Drawing.Brushes.White, 0, 0, rectf.Width, rectf.Height);
                g.DrawString(TicketHeader + "\r\n" + priceString + "\r\n" + DateTime.Now.ToString("ddd, dd. MMM yyyy H:mm:ss"), new Font("Tahoma", 70), System.Drawing.Brushes.Black, rectf);

                g.Flush();
                createdBitmap.Save("createdBitmap.bmp");

                createdBitmap = FloydSteinberg(createdBitmap, createdBitmap.Width, createdBitmap.Height);
                createdBitmap.Save("createdBitmapFS.bmp");

                printer.PrintMemoryBitmap(PrinterStation.Receipt, createdBitmap, -11, -2);
                printer.CutPaper(100);
            }
            printer.DeviceEnabled = false;
            printer.Release();
            printer.Close();
            Session.Instance.PosPrinter = null;

            if (Session.Instance.DepositAmount > Session.Instance.Price)
            {
                int change = Session.Instance.DepositAmount - Session.Instance.Price;

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Head.Text = String.Format("Returning cash {0:0.00}", ((float)change)/100);
                }));
                CashChanger cashChanger = Session.Instance.CashChanger;
                if (cashChanger != null)
                {
                    try
                    {
                        cashChanger.DispenseChange(change);
                    }
                    catch (Exception ex)
                    {
                        if (cashChanger.State != ControlState.Closed)
                            cashChanger.Close();
                        MessageBox.Show("Cash Unit out of order");
                    }
                }
                else
                {
                    MessageBox.Show("Cash Unit not activated");
                }
            }

            Dispatcher.BeginInvoke(new Action(() =>
            {
                Head.Text = doneHead;
                Subtitle.Text = ticketsToPrint > 1 ? Subtitle.Text = doneMulMsg : doneSingleMsg;
            }));
            
            //PosCommon doorhub = null;

            //var commonCollection = explorer.GetDevices("PosCommon");
            //foreach (DeviceInfo deviceInfo in commonCollection)
            //{
            //    if (deviceInfo.ServiceObjectName != "DoorHub")
            //        continue;
            //    doorhub = (PosCommon)explorer.CreateInstance(deviceInfo);
            //    break;
            //}
            //if (doorhub == null)
            //{
            //    MessageBox.Show("Doorhub not activated");
            //    return;
            //}

            //doorhub.Open();
            //doorhub.Claim(1000);
            //doorhub.DeviceEnabled = true;

            //DirectIOData data = doorhub.DirectIO(1, 0, new string[] { "HexMaskGPB", "40" });

            //doorhub.DeviceEnabled = false;
            //doorhub.Release();
            //doorhub.Close();

            Session.Instance.DepositAmount = 0;
            Session.Instance.Price = 0;
            Session.Instance.Ticket = "";
            Thread.Sleep(5000);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                isLeavePageAllowed = true;
                DefaultLinkNavigator dln = new DefaultLinkNavigator();
                dln.Navigate(new Uri("/Pages/Home.xaml", UriKind.Relative), this);
            }));
        }

        private static Bitmap FloydSteinberg(Bitmap image, int maxWidth, int maxHeight)
        {
            // initial error check
            if (image.PixelFormat == PixelFormat.Format1bppIndexed)
                return image;

            BitmapData bmpData = null;
            int outputBitmapWidth = image.Width > maxWidth ? maxWidth : image.Width;
            int outputBitmapHeight = image.Height > maxHeight ? maxHeight : image.Height;
            System.Drawing.Rectangle outputRect = new System.Drawing.Rectangle(0, 0, outputBitmapWidth, outputBitmapHeight);
            Bitmap grayBitmap = new Bitmap(outputBitmapWidth, outputBitmapHeight, PixelFormat.Format1bppIndexed);
            try
            {
                bmpData = image.LockBits(outputRect, ImageLockMode.ReadWrite, image.PixelFormat);
                if (image.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    #region bitmap needs conversion
                    // create a grayscale bitmap from input
                    // invert the color palette so that the bitmap does not need to be inverted later
                    ColorPalette colorPalette = grayBitmap.Palette;
                    colorPalette.Entries[0] = Color.FromArgb(255, 255, 255, 255);
                    colorPalette.Entries[1] = Color.FromArgb(255, 0, 0, 0);
                    grayBitmap.Palette = colorPalette;

                    BitmapData grayBmpData = null;
                    try
                    {
                        // create a buffer to hold temporary dithering information
                        int ditherBufferWidth = grayBitmap.Width;
                        byte[][] ditherBuffer = new[] { new byte[ditherBufferWidth + 1], new byte[ditherBufferWidth + 1] };
                        byte currentBufferRow = 0;
                        byte otherBufferRow = 1;
                        int bufferColumn = 0;

                        grayBmpData = grayBitmap.LockBits(outputRect, ImageLockMode.ReadWrite, grayBitmap.PixelFormat);
                        unsafe
                        {
                            int ditherHeight =  grayBitmap.Height - 1;
                            int ditherWidth = grayBitmap.Width - 1;
                           
                            
                            byte* grayRow;
                            byte bitCounter;
                            byte currentByte;
                            for (int y = 0; y < bmpData.Height; y++)
                            {
                                #region dither the image (floyd steinberg)
                                byte* row = (byte*)bmpData.Scan0 + (y * bmpData.Stride);
                                for (int x = 0; x < bmpData.Stride; x += 3)
                                {
                                    byte luma = (byte)(row[x] * 0.114 + row[x + 1] * 0.587 + row[x + 2] * 0.299);
                                    ditherBuffer[currentBufferRow][bufferColumn++] = luma;
                                }

                                if (y > 0 && y < ditherHeight)
                                {
                                    // dither the row
                                    for (int x = 1; x < ditherWidth; x++)
                                    {
                                        int divisor;
                                        if (ditherBuffer[otherBufferRow][x] < 128)
                                        {
                                            divisor = ditherBuffer[otherBufferRow][x] / 16;
                                            ditherBuffer[otherBufferRow][x] = 0;
                                        }
                                        else
                                        {
                                            divisor = (ditherBuffer[otherBufferRow][x] - 255) / 16;
                                            ditherBuffer[otherBufferRow][x] = 1;
                                        }
                                        ditherBuffer[currentBufferRow][x - 1] += (byte)(divisor * 3);
                                        ditherBuffer[currentBufferRow][x] += (byte)(divisor * 5);
                                        ditherBuffer[currentBufferRow][x + 1] += (byte)divisor;
                                        ditherBuffer[otherBufferRow][x + 1] += (byte)(divisor * 7);
                                    }

                                    // write the dithered row to the new image
                                    grayRow = (byte*)grayBmpData.Scan0 + ((y - 1) * grayBmpData.Stride);
                                    bitCounter = 0;
                                    currentByte = 0x00;
                                    for (int x = 0; x < ditherBufferWidth; x++)
                                    {
                                        currentByte = (byte)(currentByte << 1);
                                        if (ditherBuffer[otherBufferRow][x] == 0)
                                            currentByte |= 0x01;
                                        if (bitCounter == 7)
                                        {
                                            *grayRow = currentByte;
                                            grayRow++;
                                            bitCounter = 0;
                                            currentByte = 0x00;
                                        }
                                        else
                                            bitCounter++;
                                    }

                                    // the last byte in a row might be incomplete
                                    if ((bitCounter > 0) && (bitCounter < 7))
                                    {
                                        currentByte = (byte)(currentByte << (7 - bitCounter));
                                        *grayRow = currentByte;
                                    }
                                }

                                // alternating change of the current row
                                otherBufferRow = currentBufferRow;
                                currentBufferRow = (byte)((currentBufferRow == 0) ? 1 : 0);
                                bufferColumn = 0;
                                #endregion
                            }

                            #region writing the last dithered row to the image
                            grayRow = (byte*)grayBmpData.Scan0 + (ditherHeight * grayBmpData.Stride);
                            bitCounter = 0;
                            currentByte = 0x00;
                            for (int x = 0; x < ditherBufferWidth; x++)
                            {
                                currentByte = (byte)(currentByte << 1);
                                if (ditherBuffer[otherBufferRow][x] == 0)
                                    currentByte |= 0x01;
                                if (bitCounter == 7)
                                {
                                    *grayRow = currentByte;
                                    grayRow++;
                                    bitCounter = 0;
                                    currentByte = 0x00;
                                }
                                else
                                    bitCounter++;
                            }

                            // the last byte in a row might be incomplete
                            if ((bitCounter > 0) && (bitCounter < 7))
                            {
                                currentByte = (byte)(currentByte << (7 - bitCounter));
                                *grayRow = currentByte;
                            }
                            #endregion
                        }
                    }
                    finally
                    {
                        grayBitmap.UnlockBits(grayBmpData);
                    }
                    #endregion
                }
            }
            finally
            {
                if (bmpData != null)
                    image.UnlockBits(bmpData);
            }

            return grayBitmap;
        }

    }
}
