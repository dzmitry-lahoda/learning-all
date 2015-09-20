#ifndef C2_H_INCLUDED
#define C2_H_INCLUDED
#include "Focs.h"

void printCharList(CharList list){
   if (list == NULL){
      printf("\n");
     return;
   }
   printf("%c",list->element);
   printCharList(list->next);

}

///
// Returns true if `first` precedes `second` lexicographycally
///
int focs_223_precedes(CharList first,CharList second){
  //printCharList(first);
  //printCharList(second);
  if (first == NULL && second == NULL){
   return FALSE;
  }
  if (second == NULL)
    return FALSE;

  if (first->element > second->element)
    return FALSE;
  if (first->element < second->element)
    return TRUE;
  focs_223_precedes(first->next,second->next);
}

CharList createCharList(){
 return (CharList)malloc(sizeof(CharCell));
}


#endif // C2_H_INCLUDED
