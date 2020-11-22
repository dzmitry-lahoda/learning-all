namespace TimeLapseExp.Cameras
{

    public sealed class Focus
    {
        private FocusModes _mode = FocusModes.Manual;

        private int _position;

        public Focus()
        {
            
        }

        public Focus(FocusModes mode, int position)
        {
            Mode = mode;
            Position = position;
        }

        public Focus Clone()
        {
            return new Focus(Mode, _position);
        }

        /// <summary>
        /// Gets or sets focus _mode.
        /// </summary>
        public FocusModes Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }


        /// <summary>
        /// Gets or sets focus _position. From 1 to 120 is Macro focus _position, 121-240 is Normal focus _position.
        /// </summary>
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public void MovePosition(int positionStep)
        {
            Position += positionStep;
        }
    }
}
