using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using TimeLapseExp.Cameras;
using TimeLapseExp.Instrumentation;

namespace TimeLapseExp.WinFormsGUI
{
    internal partial class FormExp : Form
    {
        private void buttonTestChart_Click(object sender, EventArgs e)
        {
            
            var info = new Dictionary<string, FocusExposureContrastShannon>();
            var read = new List<String>(File.ReadAllLines(@"D:\et10000_200000\entropy.txt"));
            var floats = new List<double>(20);
            read.ForEach(str => floats.Add(Double.Parse(str)));
            var step = 10000;
            for (var i=0;i<20;i=i+1)
            {
                var ce = 10000 + i * step;
                info.Add("p" + i, new FocusExposureContrastShannon() { Exposure = ce, Shannon = floats[i] });
           
            }
          
          Helper.ShowChart("Shannon Entropy", info);
        }

        private void buttonTestSerialization_Click(object sender, EventArgs e)
        {
            //            var serializer = new XmlSerializer(
            //                typeof(SessionInfo),
            //                new Type[]{typeof(SequenceInfo),typeof(CameraInfo),typeof(CaptureInfo),typeof(Focus),typeof(Capture),typeof(Exposure),typeof(MemoryCardInfo)}
            //                );
            var captureInfo = new SessionInfo();
            var serializer = new XmlSerializer(
                typeof(SessionInfo),
                new[] {typeof(SequenceInfo),typeof(CameraInfo),typeof(MemoryCardInfo), typeof(CaptureInfo), typeof(Focus), typeof(Capture), typeof(Exposure) });
            //var soapFormatter = new SoapFormatter();
            var fileName = FileNamingEngine.GetExperimentSetupFileName() + "experiment" + Model.ExperimentCount + "setups.txt";
            var filePath = Path.Combine(Model.StoreDirectory, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(stream, captureInfo);
            }
        }

    }
}
