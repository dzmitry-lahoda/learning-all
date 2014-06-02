using System;
using System.Drawing;
using System.Windows.Forms;

namespace Scand.Toolbars
{
    /// <summary>
    /// Represents a selectable ToolStripButton  that can contain
    //     text and images with Uris.
    /// </summary>
    public partial class ToolbarButton : ToolStripButton
    {
        private Uri imageUri;

        private bool hasImageUri;

        private bool imageSetManually;

        public ToolbarButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets and sets uri of an image.
        /// </summary>
        public Uri ImageUri
        {
            get { return imageUri; }
            set
            {
                imageUri = value;
            }
        }

        public void SetImage(Image image,bool manually)
        {
            Image = image;
            ImageSetManually = manually;
        }

        /// <summary>
        /// True if image was aligned to button.
        /// </summary>
        public bool ImageSetManually
        {
            get { return imageSetManually; }
            set { imageSetManually = value; }
        }
    }
}
