using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeLapseExp.Cameras.Enums
{
    public enum ErrorCodes
    {
    NoError = 0,             /* 00 */
    InvalidParameter,        /* 01 */
    InvalidParamRange,      /* 02 */
    CommPortNotAvailable,  /* 03 */
    CommFailure,             /* 04 */
    InsuffDiskSpace,        /* 05 */
    UserDisconnect,          /* 06 */
    BadClose,                /* 07 */
    BadWrite,    			  /* 08 */
    BadExecute,     		  /* 09 */
    BadRead,     			  /* 0A */
    BadOpen, 			      /* 0B */
    CameraNotResponding,    /* 0C */
    CameraBadResponse,      /* 0D */
    Nak1Response,            /* 0E */
    Dc1Response,             /* 0F */
    BadChecksum,             /* 10 */
    BadGetlong,              /* 11 */
    BadRcvlen,               /* 12 */
    UserBreak,               /* 13 */
    SessionClosed,           /* 14 */
    UsbNotResponding,		  /* 15 */
	OsNotSupport,
	DuplicateConnect,
	BadCamNo,
	DeviceOpen,
	NotConnected,
	MemoryFull,
	AspiNotFound,
	NotInitialize,
	CameraBusy,
	XferCancelled,
	AspiError,
	ErrorUnknown=255           /* FF */
} 
}
