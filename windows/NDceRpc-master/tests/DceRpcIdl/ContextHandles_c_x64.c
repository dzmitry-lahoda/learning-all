

/* this ALWAYS GENERATED file contains the RPC client stubs */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Thu May 22 13:35:57 2014
 */
/* Compiler settings for ContextHandles.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 7.00.0555 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#if defined(_M_AMD64)


#pragma warning( disable: 4049 )  /* more than 64k source lines */
#if _MSC_VER >= 1200
#pragma warning(push)
#endif

#pragma warning( disable: 4211 )  /* redefine extern to static */
#pragma warning( disable: 4232 )  /* dllimport identity*/
#pragma warning( disable: 4024 )  /* array to pointer mapping*/

#include <string.h>

#include "ContextHandles_h.h"

#define TYPE_FORMAT_STRING_SIZE   43                                
#define PROC_FORMAT_STRING_SIZE   145                               
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   0            

typedef struct _ContextHandles_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } ContextHandles_MIDL_TYPE_FORMAT_STRING;

typedef struct _ContextHandles_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } ContextHandles_MIDL_PROC_FORMAT_STRING;

typedef struct _ContextHandles_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } ContextHandles_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};


extern const ContextHandles_MIDL_TYPE_FORMAT_STRING ContextHandles__MIDL_TypeFormatString;
extern const ContextHandles_MIDL_PROC_FORMAT_STRING ContextHandles__MIDL_ProcFormatString;
extern const ContextHandles_MIDL_EXPR_FORMAT_STRING ContextHandles__MIDL_ExprFormatString;

#define GENERIC_BINDING_TABLE_SIZE   0            


/* Standard interface: ContextHandles, ver. 0.1,
   GUID={0xC65271F0,0x19B4,0x4B35,{0xAE,0x88,0xFA,0x53,0xD6,0xB2,0x70,0x80}} */



static const RPC_CLIENT_INTERFACE ContextHandles___RpcClientInterface =
    {
    sizeof(RPC_CLIENT_INTERFACE),
    {{0xC65271F0,0x19B4,0x4B35,{0xAE,0x88,0xFA,0x53,0xD6,0xB2,0x70,0x80}},{0,1}},
    {{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}},
    0,
    0,
    0,
    0,
    0,
    0x00000000
    };
RPC_IF_HANDLE ContextHandles_v0_1_c_ifspec = (RPC_IF_HANDLE)& ContextHandles___RpcClientInterface;

extern const MIDL_STUB_DESC ContextHandles_StubDesc;

static RPC_BINDING_HANDLE ContextHandles__MIDL_AutoBindHandle;


short RemoteOpen( 
    /* [out] */ PPCONTEXT_HANDLE_TYPE pphContext,
    /* [string][in] */ unsigned char *pszFile)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&ContextHandles_StubDesc,
                  (PFORMAT_STRING) &ContextHandles__MIDL_ProcFormatString.Format[0],
                  pphContext,
                  pszFile);
    return ( short  )_RetVal.Simple;
    
}


void RemoteRead( 
    /* [in] */ PCONTEXT_HANDLE_TYPE phContext,
    /* [in] */ int cbBuf,
    /* [size_is][out] */ unsigned char achBuf[  ],
    /* [out][in] */ short *pcbBuf)
{

    NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&ContextHandles_StubDesc,
                  (PFORMAT_STRING) &ContextHandles__MIDL_ProcFormatString.Format[44],
                  phContext,
                  cbBuf,
                  achBuf,
                  pcbBuf);
    
}


short RemoteClose( 
    /* [out][in] */ PPCONTEXT_HANDLE_TYPE pphContext)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&ContextHandles_StubDesc,
                  (PFORMAT_STRING) &ContextHandles__MIDL_ProcFormatString.Format[100],
                  pphContext);
    return ( short  )_RetVal.Simple;
    
}


#if !defined(__RPC_WIN64__)
#error  Invalid build platform for this stub.
#endif

static const ContextHandles_MIDL_PROC_FORMAT_STRING ContextHandles__MIDL_ProcFormatString =
    {
        0,
        {

	/* Procedure RemoteOpen */

			0x33,		/* FC_AUTO_HANDLE */
			0x48,		/* Old Flags:  */
/*  2 */	NdrFcLong( 0x0 ),	/* 0 */
/*  6 */	NdrFcShort( 0x0 ),	/* 0 */
/*  8 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 10 */	NdrFcShort( 0x0 ),	/* 0 */
/* 12 */	NdrFcShort( 0x3e ),	/* 62 */
/* 14 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x3,		/* 3 */
/* 16 */	0xa,		/* 10 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 18 */	NdrFcShort( 0x0 ),	/* 0 */
/* 20 */	NdrFcShort( 0x0 ),	/* 0 */
/* 22 */	NdrFcShort( 0x0 ),	/* 0 */
/* 24 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter pphContext */

/* 26 */	NdrFcShort( 0x110 ),	/* Flags:  out, simple ref, */
/* 28 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 30 */	NdrFcShort( 0x6 ),	/* Type Offset=6 */

	/* Parameter pszFile */

/* 32 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 34 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 36 */	NdrFcShort( 0xc ),	/* Type Offset=12 */

	/* Return value */

/* 38 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 40 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 42 */	0x6,		/* FC_SHORT */
			0x0,		/* 0 */

	/* Procedure RemoteRead */

/* 44 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 46 */	NdrFcLong( 0x0 ),	/* 0 */
/* 50 */	NdrFcShort( 0x1 ),	/* 1 */
/* 52 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 54 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 56 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 58 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 60 */	NdrFcShort( 0x46 ),	/* 70 */
/* 62 */	NdrFcShort( 0x1a ),	/* 26 */
/* 64 */	0x41,		/* Oi2 Flags:  srv must size, has ext, */
			0x4,		/* 4 */
/* 66 */	0xa,		/* 10 */
			0x3,		/* Ext Flags:  new corr desc, clt corr check, */
/* 68 */	NdrFcShort( 0x1 ),	/* 1 */
/* 70 */	NdrFcShort( 0x0 ),	/* 0 */
/* 72 */	NdrFcShort( 0x0 ),	/* 0 */
/* 74 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter phContext */

/* 76 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 78 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 80 */	NdrFcShort( 0xe ),	/* Type Offset=14 */

	/* Parameter cbBuf */

/* 82 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 84 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 86 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter achBuf */

/* 88 */	NdrFcShort( 0x13 ),	/* Flags:  must size, must free, out, */
/* 90 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 92 */	NdrFcShort( 0x12 ),	/* Type Offset=18 */

	/* Parameter pcbBuf */

/* 94 */	NdrFcShort( 0x158 ),	/* Flags:  in, out, base type, simple ref, */
/* 96 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 98 */	0x6,		/* FC_SHORT */
			0x0,		/* 0 */

	/* Procedure RemoteClose */

/* 100 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 102 */	NdrFcLong( 0x0 ),	/* 0 */
/* 106 */	NdrFcShort( 0x2 ),	/* 2 */
/* 108 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 110 */	0x30,		/* FC_BIND_CONTEXT */
			0xe0,		/* Ctxt flags:  via ptr, in, out, */
/* 112 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 114 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 116 */	NdrFcShort( 0x38 ),	/* 56 */
/* 118 */	NdrFcShort( 0x3e ),	/* 62 */
/* 120 */	0x44,		/* Oi2 Flags:  has return, has ext, */
			0x2,		/* 2 */
/* 122 */	0xa,		/* 10 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 124 */	NdrFcShort( 0x0 ),	/* 0 */
/* 126 */	NdrFcShort( 0x0 ),	/* 0 */
/* 128 */	NdrFcShort( 0x0 ),	/* 0 */
/* 130 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter pphContext */

/* 132 */	NdrFcShort( 0x118 ),	/* Flags:  in, out, simple ref, */
/* 134 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 136 */	NdrFcShort( 0x26 ),	/* Type Offset=38 */

	/* Return value */

/* 138 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 140 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 142 */	0x6,		/* FC_SHORT */
			0x0,		/* 0 */

			0x0
        }
    };

static const ContextHandles_MIDL_TYPE_FORMAT_STRING ContextHandles__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */
/*  2 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/*  4 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6) */
/*  6 */	0x30,		/* FC_BIND_CONTEXT */
			0xa0,		/* Ctxt flags:  via ptr, out, */
/*  8 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 10 */	
			0x11, 0x8,	/* FC_RP [simple_pointer] */
/* 12 */	
			0x22,		/* FC_C_CSTRING */
			0x5c,		/* FC_PAD */
/* 14 */	0x30,		/* FC_BIND_CONTEXT */
			0x41,		/* Ctxt flags:  in, can't be null */
/* 16 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 18 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 20 */	NdrFcShort( 0x1 ),	/* 1 */
/* 22 */	0x28,		/* Corr desc:  parameter, FC_LONG */
			0x0,		/*  */
/* 24 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 26 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 28 */	0x2,		/* FC_CHAR */
			0x5b,		/* FC_END */
/* 30 */	
			0x11, 0x8,	/* FC_RP [simple_pointer] */
/* 32 */	0x6,		/* FC_SHORT */
			0x5c,		/* FC_PAD */
/* 34 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 36 */	NdrFcShort( 0x2 ),	/* Offset= 2 (38) */
/* 38 */	0x30,		/* FC_BIND_CONTEXT */
			0xe1,		/* Ctxt flags:  via ptr, in, out, can't be null */
/* 40 */	0x0,		/* 0 */
			0x0,		/* 0 */

			0x0
        }
    };

static const unsigned short ContextHandles_FormatStringOffsetTable[] =
    {
    0,
    44,
    100
    };


static const MIDL_STUB_DESC ContextHandles_StubDesc = 
    {
    (void *)& ContextHandles___RpcClientInterface,
    MIDL_user_allocate,
    MIDL_user_free,
    &ContextHandles__MIDL_AutoBindHandle,
    0,
    0,
    0,
    0,
    ContextHandles__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x50002, /* Ndr library version */
    0,
    0x700022b, /* MIDL Version 7.0.555 */
    0,
    0,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };
#if _MSC_VER >= 1200
#pragma warning(pop)
#endif


#endif /* defined(_M_AMD64)*/

