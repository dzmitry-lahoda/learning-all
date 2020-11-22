using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using NLog;
using NLog.Config;
using NLog.Targets;
using TimeLapseExp.Cameras;
using TimeLapseExp.Instrumentation;

namespace TimeLapseExp.WinFormsGUI
{
    internal partial class FormExp
    {

        private Logger _loggerSession;

        private Logger _loggerContrastMeasurment;

        private Logger _loggerShannonEntropy;

        private const String ExperimentDelimeter = "------------------------------------------";

        /// <summary>
        /// Initilize logging functionality.
        /// </summary>
        public void InitializeLoggers()
        {
            _loggerContrastMeasurment = ConfigureLogger("ContrastMeasurmentLogger",richTextBoxContrastMeasurment,"contrast.txt");
            _loggerShannonEntropy = ConfigureLogger("ShannonEntropyLogger", richTextBoxShannonEntropy, "shannon.txt");
            _loggerSession = ConfigureLogger("SessionLogger", richTextBoxSession, "session.txt");
        }

        private Logger ConfigureLogger(
            String loggerName,
            RichTextBox target,
            String targetFileAppendix
            )
        {
            var config = new LoggingConfiguration();
            var logFactory = new LogFactory();

            var richTextBoxTarget = new RichTextBoxTarget();
            richTextBoxTarget.Layout = "${date:format=HH\\:MM\\:ss} ${message}";
            richTextBoxTarget.ControlName = target.Name;
            richTextBoxTarget.FormName = Name;
            richTextBoxTarget.UseDefaultRowColoringRules = true;

            var targetFileName =
                Path.Combine(Model.StoreDirectory, FileNamingEngine.GetPictureNamePrefix() + targetFileAppendix);
            var fileTarget = new FileTarget();
            fileTarget.FileName = targetFileName;
            fileTarget.Name = fileTarget.FileName;

            config.AddTarget(richTextBoxTarget.ControlName, richTextBoxTarget);
            config.AddTarget(fileTarget.Name, fileTarget);
            
            var ruleRichTextBox = new LoggingRule("*", LogLevel.Info, richTextBoxTarget);
            config.LoggingRules.Add(ruleRichTextBox);

            var ruleFileTarget = new LoggingRule("*", LogLevel.Trace, fileTarget);
            config.LoggingRules.Add(ruleFileTarget);

            logFactory.Configuration = config;
            var logger = logFactory.GetLogger(loggerName);
            return logger;
            
        }

        private void DoBeforeStartCapturingSetups(SessionInfo sessionInfo)
        {
            var serializer = new XmlSerializer(
                typeof(SessionInfo),
                new[] { typeof(SequenceInfo), typeof(CameraInfo), typeof(MemoryCardInfo), typeof(CaptureInfo), typeof(Focus), typeof(Capture), typeof(Exposure) });
            var fileName = FileNamingEngine.GetExperimentSetupFileName() + "experiment" + Model.ExperimentCount + "setups.txt";
            var filePath = Path.Combine(Model.StoreDirectory, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(stream, sessionInfo);
            }
            Presenter.SetLastSessionInfo(sessionInfo);
            Presenter.IncreaseExperimentCount();
        }
    }
}
