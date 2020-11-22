#include <InitGuid.h>


// {A7BF37B6-BD2B-4F6C-982F-565ABDA66A61}
DEFINE_GUID(CLSID_INativeOutOfProcServer, 0xa7bf37b6, 0xbd2b, 0x4f6c, 0x98, 0x2f, 0x56, 0x5a, 0xbd, 0xa6, 0x6a, 0x61);

// {88BA2D59-B6D3-456A-A64C-4868F91A9B55}
DEFINE_GUID(IID_INativeOutOfProcServerInterface, 0x88ba2d59, 0xb6d3, 0x456a, 0xa6, 0x4c, 0x48, 0x68, 0xf9, 0x1a, 0x9b, 0x55);


typedef struct _INativeOutOfProcServer INativeOutOfProcServer;

typedef HRESULT STDMETHODCALLTYPE QueryInterfacePtr( INativeOutOfProcServer *, REFIID, void **);
typedef ULONG STDMETHODCALLTYPE AddRefPtr( INativeOutOfProcServer *);
typedef ULONG STDMETHODCALLTYPE ReleasePtr( INativeOutOfProcServer *);
typedef HRESULT STDMETHODCALLTYPE SetStatePtr(INativeOutOfProcServer* ,char* );
typedef HRESULT STDMETHODCALLTYPE GetStatePtr(INativeOutOfProcServer* ,char* );

typedef struct {
    QueryInterfacePtr  * QueryInterface;
    AddRefPtr          * AddRef;
    ReleasePtr         * Release;
    SetStatePtr* SetState;
     GetStatePtr* GetState;
} INativeOutOfProcServerInterface;

static  INativeOutOfProcServerInterface* INativeOutOfProcServerInterface_vtbl;

 struct _INativeOutOfProcServer {
    INativeOutOfProcServerInterface* vtbl;
    long count;
    char state[100];
}  ;

INativeOutOfProcServerInterface InitNativeOutOfProcSrv(void);
