using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeLapseExp.Cameras;

namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// This class class stores info about camera setups for taking short sequence of pictures so fast,
    /// as it possible.
    /// </summary>
    //TODO:Implemtns ToString, Equal, GetHashCode and Clone methods.
    [Serializable]
    public class SequenceInfo
    {

        private CameraInfo _cameraInfo = new CameraInfo();

        private SequencePriorities _sequencePriority = SequencePriorities.Exposure;

        private int _exposureSequenceCount;
        private int _exposureTimeStep;
        private int _focusSequenceCount;
        private int _focusPositionStep;

        private int[] _exposureTimes = GenerataSequence(10000,20000,30);
        private int[] _focusPositions = GenerataSequence(10,10,20);

        public SequenceInfo()
        {
            
        }

        public SequenceInfo(CameraInfo cameraInfo, SequencePriorities sequencePriority, int exposureSequenceCount, int exposureTimeStep, int focusSequenceCount, int focusPositionStep)
        {
            _cameraInfo = cameraInfo;
            _sequencePriority = sequencePriority;
            _exposureSequenceCount = exposureSequenceCount;
            _exposureTimeStep = exposureTimeStep;
            _focusSequenceCount = focusSequenceCount;
            _focusPositionStep = focusPositionStep;
            _exposureTimes = GenerataSequence(_cameraInfo.CaptureInfo.Exposure.Time, _exposureTimeStep, exposureSequenceCount);
            _focusPositions = GenerataSequence(_cameraInfo.CaptureInfo.Focus.Position, _focusPositionStep, focusSequenceCount);
        }

        private static int[] GenerataSequence(int initialValue,int step,int length)
        {
            var collection = new List<int>();
            for (var i = 0; i < length; i++)
            {
                collection.Add(initialValue + i * step);
            }
            return collection.ToArray();
        }

        public CameraInfo CameraInfo
        {
            get { return _cameraInfo; }
            set { _cameraInfo = value; }
        }

        public SequencePriorities SequencePriority
        {
            get { return _sequencePriority; }
            set { _sequencePriority = value; }
        }

        public int ExposureSequenceCount
        {
            get { return _exposureSequenceCount; }
            set { _exposureSequenceCount = value; }
        }

        public int ExposureTimeStep
        {
            get { return _exposureTimeStep; }
            set { _exposureTimeStep = value; }
        }

        public int FocusSequenceCount
        {
            get { return _focusSequenceCount; }
            set { _focusSequenceCount = value; }
        }

        public int FocusPositionStep
        {
            get { return _focusPositionStep; }
            set { _focusPositionStep = value; }
        }

        public int SummedCount
        {
            get
            {
                return _focusSequenceCount*_exposureSequenceCount;
            }
        }

        public int[] FocusPositions
        {
            get
            {
                var clone = (int[])_focusPositions.Clone();
                return clone;
            }
            set
            {
                _focusPositions = value;
            }
        }

        public int[] ExposureTimes
        {
            get
            {
                var clone = (int[])_exposureTimes.Clone();
                return clone;
            }
            set
            {
                _exposureTimes = value;
            }
        }
    }
}
