#include <stdio.h>
#include <stdlib.h>
#include "Focs.h"
#include "c2.h"


typedef
struct tagCELL // <-- identifier
CELL // alias/name
;

typedef
struct tagCELL* // <-- identifier
LIST // alias/name
;
//typedef
struct tagCELL// identifier in tag namespace
{
  int element;
  LIST next;
};





int main()
{
    //Create list
    CELL c;
    c.element = 1;
    IntList a = (IntList)malloc(sizeof(IntCell));
    a->element = 42;

//2.2.3
CharList f1 = createCharList();
CharList f2 = createCharList();
CharList f3 = createCharList();
CharList f4 = createCharList();
f1->element = 'b';
f1->next  = f2;
f2->element = 'a';
f2->next  = f3;
f3->element = 's';
f3->next  = f4;
f4->element = 'e';
f4->next = NULL;


CharList s1 = createCharList();
CharList s2 = createCharList();
CharList s3 = createCharList();
CharList s4 = createCharList();
s1->element = 'b';
s1->next  = s2;
s2->element = 'a';
s2->next  = s3;
s3->element = 'l';
s3->next  = s4;
s4->element = 'l';
s4->next = NULL;

printCharList(f1);
int shouldBeTrue1 = 'e' < 'l'; //focs_223_precedes(f4,s4);
printf("%d\n",shouldBeTrue1);

int shouldBeFalse1 = focs_223_precedes(s4,f4);
printf("%d\n",shouldBeFalse1);


int shouldBeFalse = focs_223_precedes(f1,s1);
printf("%d\n",shouldBeFalse);
int shouldBeTrue = focs_223_precedes(s1,f1);
printf("%d\n",shouldBeTrue);

    printf("Hello world!\n");
    return 0;
}

