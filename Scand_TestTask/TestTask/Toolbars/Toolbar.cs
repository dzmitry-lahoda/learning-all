#define THREADS

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Scand.Toolbars
{
    /// <summary>
    /// Toolbar wich provides load it's buttons from XML. 
    /// Button can contain text or image.
    /// Images downloaded asynchronously.
    /// </summary>
    public partial class Toolbar : UserControl
    {
        private readonly object syncRoot = new object();

        /// <summary>
        /// Set in true if <see cref="LoadXml"/> was succesfully called.
        /// </summary>
        private bool inicialized;

        public Toolbar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads XML file from URI. Add buttons to toolbar, depending on XML content.
        /// Sets images asynchronous.
        /// Earlier added buttons are deleted.
        /// </summary>
        /// <param name="xmlUri">Uri pointing to XML file with toolbar buttons descriptions</param>
        /// <exception cref="UriFormatException">Thrown when when xmlUri has wrong format. </exception>
        /// <exception cref="XmlException">Thrown when Xml, provided by URI, has wrong format.</exception>
        /// <exception cref="WrongXmlDataException">Thrown when XML data, wich describes toolbar configuration,
        ///  has invalid values or misses needed.</exception>
        ///<exception cref="WebException">Thrown when it is not possible to dowload image for button.</exception>
        public void LoadXml(string xmlUri)
        {
            IEnumerable<ToolbarButton> buttons;
#if THREADS
            lock (syncRoot)
#endif
            {
                inicialized = false;
                toolStripWithButtons.Items.Clear();
                var uri = new Uri(xmlUri);
                buttons = ToolbarXmlParser.GetToolbarButtons(uri);
                AddButtons(buttons);
                inicialized = true;
            }
#if THREADS
            ThreadPool.QueueUserWorkItem(delegate
#endif
                                        {
                                            DownloadAndSetImages(buttons);                           
                                        }
#if THREADS
);
#endif

        }

        /// <summary>
        /// Sets image for button. Downloads it using URI.
        /// </summary>
        /// <param name="buttonId">Id of button.</param>
        /// <param name="imageUri">URI which points to an image.</param>
        /// <exception cref="UriFormatException">Thrown when URI has wrong format.</exception>
        /// <exception cref="ArgumentException">Thrown when buttonId is wrong.</exception>
        ///<exception cref="WebException">Thrown when image can't be downloaded.</exception>
        public void SetButtonImage(string buttonId, string imageUri)
        {
            if (String.IsNullOrEmpty(buttonId))
            {
                throw new ArgumentException("Id of button can't be null or empty.", "buttonId");
            }
            ToolStripItem[] finded;
#if THREADS
            lock (syncRoot)
#endif
            {
                if (!inicialized)
                {
                    throw new InvalidOperationException("Toolbar was not inicialized by calling LoadXml method.");
                }
                finded = toolStripWithButtons.Items.Find(buttonId,false);
            }
            if (finded.Length == 0)
            {
                var message = String.Format("Current toolbar does not have the button with specified id=\"{0}\".", buttonId);
                throw new ArgumentException(message, "buttonId");
            }
            var button = (ToolbarButton)finded[0];
            var uri = new Uri(imageUri);
#if THREADS
            ThreadPool.QueueUserWorkItem(delegate
#endif
                                         {
                                             var image = DownloadImage(uri);
#if THREADS
                                             lock (syncRoot)
#endif
                                             {
                                                 SetImage(button, image, uri, true);
                                             }
                                         }
#if THREADS
);
#endif
        }

        /// <summary>
        /// Downloads image from web server.
        /// </summary>
        /// <param name="imageUri"></param>
        /// <returns></returns>
        private static Image DownloadImage(Uri imageUri)
        {
            byte[] buffer;
            using (var client = new WebClient())
            {
                try
                {
                    buffer = client.DownloadData(imageUri);
                }
                catch (WebException ex)
                {
                    var message = String.Format(
                        "Failed to download image from \"{0}\". Reason:{1}", imageUri, Environment.NewLine + ex.Message
                        );
                    throw new WebException(message, ex);
                }
            }
            using (var stream = new MemoryStream(buffer))
            {
                var image = Image.FromStream(stream);
                return image;
            }
        }

        //TODO: Determine behaviour.
        private static void SetImage(ToolbarButton button, Image image, Uri imageUri, bool manually)
        {
            if (button.ImageSetManually && manually == false)
            {
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                return;
            }
            button.Image = image;
            if (imageUri != null)
            {
                button.ImageUri = imageUri;
            }
            button.ImageSetManually = manually;
        }

        /// <summary>
        /// Downloads images for buttons and shows them.
        /// </summary>
        private void DownloadAndSetImages(IEnumerable<ToolbarButton> buttons)
        {
            var memory = new Dictionary<ToolbarButton, Image>();
            foreach (var button in buttons)
            {
                if (button.ImageUri != null)
                {
                    var image = DownloadImage(button.ImageUri);
                    memory.Add(button, image);
                }
            }
#if THREADS
            lock (syncRoot)
#endif
            {
                //TODO: Fix the bug without touching entire toolStripWithButtons.Items collection.
                toolStripWithButtons.Items.Clear();
                foreach (var item in memory)
                {
                    SetImage(item.Key, item.Value, null, false);
                }
                //Invalidate();
                foreach (var button in buttons)
                {
                    toolStripWithButtons.Items.Add(button);
                }
            }
        }

        /// <summary>
        /// Adds buttons to <see cref="toolStripWithButtons"/> .
        /// </summary>
        private void AddButtons(IEnumerable<ToolbarButton> buttons)
        {
            foreach (var button in buttons)
            {
                toolStripWithButtons.Items.Add(button);
            }
        }
    }
}