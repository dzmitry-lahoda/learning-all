using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TimeLapseExp.Cameras;

namespace TimeLapseExp.WinFormsGUI
{   
    /// <summary>
    /// This class store info about long running capture sequence.
    /// </summary>
    //TODO:Implemtns ToString, Equal, GetHashCode and Clone methods.
    [Serializable]
    public class SessionInfo
    {
        private int _sequenceCount;

        private bool _copyImages;

        private TimeSpan _timeSpanBetweenSequences = new TimeSpan(0,0,0,60);

        private SequenceInfo _sequenceInfo = new SequenceInfo();

        private String _storeDirectory = String.Empty;


        public SessionInfo()
        {
            
        }

        public SessionInfo(int sequenceCount, TimeSpan timeSpanBetweenSequences, SequenceInfo sequenceInfo, string storeDirectory, bool copyImages)
        {
            _sequenceCount = sequenceCount;
            _timeSpanBetweenSequences = timeSpanBetweenSequences;
            _sequenceInfo = sequenceInfo;
            _storeDirectory = storeDirectory;
            _copyImages = copyImages;
        }

        public bool CopyImages
        {
            get { return _copyImages; }
            set { _copyImages = value; }
        }

        public string StoreDirectory
        {
            get { return _storeDirectory; }
            set { _storeDirectory = value; }
        }

        public SequenceInfo SequenceInfo
        {
            get { return _sequenceInfo; }
            set { _sequenceInfo = value; }
        }

        public TimeSpan TimeSpanBetweenSequences
        {
            get { return _timeSpanBetweenSequences; }
            set { _timeSpanBetweenSequences = value; }
        }

        
        public int SequenceCount
        {
            get { return _sequenceCount; }
            set { _sequenceCount = value; }
        }

        public int ConsideredTotalNumberOfPictures
        {
            get
            {
                var value = SequenceCount * SequenceInfo.SummedCount;
                return value;
                
            }
        }
    }
}
