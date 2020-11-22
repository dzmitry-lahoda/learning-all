#include <windows.h>
#include <objbase.h>
#include <stdio.h>
#include <string.h>
#include <tchar.h>
#include <Guiddef.h>
#include "NativeOutOfProcServer.h"
#include <Unknwn.h>

HRESULT STDMETHODCALLTYPE SetState( INativeOutOfProcServer* self,char* state){
    int old = sizeof(state);

    char counter[5];
    counter[0]='\0';

    sprintf(counter,"%d",self->count);
    
    char value[10];
    value[0]='\0';
    strcpy(value,state);
    strcat(value,counter);
    strcpy(self->state,value);
    return S_OK;
}

HRESULT STDMETHODCALLTYPE QueryInterfaceNativeOutOfProcSrv(INativeOutOfProcServer* self, 
                          REFIID riid, void **ppv)
{

    if (!IsEqualIID(riid, IID_INativeOutOfProcServerInterface)
        && !IsEqualIID(riid, IID_IUnknown))
   {
      *ppv = 0;
      return(E_NOINTERFACE);
   }


   *ppv = self;


   self->vtbl->AddRef(self);


   return(NOERROR);
}

HRESULT STDMETHODCALLTYPE GetState( INativeOutOfProcServer* self,char* state){
    strcpy(state,self->state);
    return S_OK;
}

ULONG STDMETHODCALLTYPE AddRefNativeOutOfProcSrv(INativeOutOfProcServer *self)
{
   ++self->count;

   return(self->count);
}

ULONG STDMETHODCALLTYPE ReleaseNativeOutOfProcSrv(INativeOutOfProcServer *self)
{
   --self->count;

   if (self->count == 0)
   {
      GlobalFree(self);
      return(0);
   }

   return(self->count);
}

INativeOutOfProcServerInterface InitNativeOutOfProcSrv(void){
    INativeOutOfProcServerInterface vtbl = {QueryInterfaceNativeOutOfProcSrv,AddRefNativeOutOfProcSrv,ReleaseNativeOutOfProcSrv,SetState,GetState};
    INativeOutOfProcServerInterface_vtbl = &vtbl;
    return vtbl;
}



