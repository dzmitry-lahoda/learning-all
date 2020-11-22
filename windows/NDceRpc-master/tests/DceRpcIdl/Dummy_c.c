

/* this ALWAYS GENERATED file contains the RPC client stubs */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Tue May 19 20:23:53 2015
 */
/* Compiler settings for Dummy.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.00.0603 
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

#include "Dummy_h.h"

#define TYPE_FORMAT_STRING_SIZE   3                                 
#define PROC_FORMAT_STRING_SIZE   31                                
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   0            

typedef struct _Dummy_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } Dummy_MIDL_TYPE_FORMAT_STRING;

typedef struct _Dummy_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } Dummy_MIDL_PROC_FORMAT_STRING;

typedef struct _Dummy_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } Dummy_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};


extern const Dummy_MIDL_TYPE_FORMAT_STRING Dummy__MIDL_TypeFormatString;
extern const Dummy_MIDL_PROC_FORMAT_STRING Dummy__MIDL_ProcFormatString;
extern const Dummy_MIDL_EXPR_FORMAT_STRING Dummy__MIDL_ExprFormatString;

#define GENERIC_BINDING_TABLE_SIZE   0            


/* Standard interface: Dummy, ver. 0.1,
   GUID={0x3D3E6558,0xA499,0x4190,{0x81,0x4A,0x7C,0x9D,0x58,0xE2,0xB8,0xC1}} */



static const RPC_CLIENT_INTERFACE Dummy___RpcClientInterface =
    {
    sizeof(RPC_CLIENT_INTERFACE),
    {{0x3D3E6558,0xA499,0x4190,{0x81,0x4A,0x7C,0x9D,0x58,0xE2,0xB8,0xC1}},{0,1}},
    {{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}},
    0,
    0,
    0,
    0,
    0,
    0x00000000
    };
RPC_IF_HANDLE Dummy_v0_1_c_ifspec = (RPC_IF_HANDLE)& Dummy___RpcClientInterface;

extern const MIDL_STUB_DESC Dummy_StubDesc;

static RPC_BINDING_HANDLE Dummy__MIDL_AutoBindHandle;


void Do( 
    /* [in] */ handle_t hBinding)
{

    NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&Dummy_StubDesc,
                  (PFORMAT_STRING) &Dummy__MIDL_ProcFormatString.Format[0],
                  hBinding);
    
}


#if !defined(__RPC_WIN64__)
#error  Invalid build platform for this stub.
#endif

static const Dummy_MIDL_PROC_FORMAT_STRING Dummy__MIDL_ProcFormatString =
    {
        0,
        {

	/* Procedure Do */

			0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/*  2 */	NdrFcLong( 0x0 ),	/* 0 */
/*  6 */	NdrFcShort( 0x0 ),	/* 0 */
/*  8 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 10 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 12 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 14 */	NdrFcShort( 0x0 ),	/* 0 */
/* 16 */	NdrFcShort( 0x0 ),	/* 0 */
/* 18 */	0x40,		/* Oi2 Flags:  has ext, */
			0x0,		/* 0 */
/* 20 */	0xa,		/* 10 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 22 */	NdrFcShort( 0x0 ),	/* 0 */
/* 24 */	NdrFcShort( 0x0 ),	/* 0 */
/* 26 */	NdrFcShort( 0x0 ),	/* 0 */
/* 28 */	NdrFcShort( 0x0 ),	/* 0 */

			0x0
        }
    };

static const Dummy_MIDL_TYPE_FORMAT_STRING Dummy__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */

			0x0
        }
    };

static const unsigned short Dummy_FormatStringOffsetTable[] =
    {
    0
    };


static const MIDL_STUB_DESC Dummy_StubDesc = 
    {
    (void *)& Dummy___RpcClientInterface,
    MIDL_user_allocate,
    MIDL_user_free,
    &Dummy__MIDL_AutoBindHandle,
    0,
    0,
    0,
    0,
    Dummy__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x50002, /* Ndr library version */
    0,
    0x800025b, /* MIDL Version 8.0.603 */
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

