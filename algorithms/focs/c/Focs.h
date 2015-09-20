#ifndef FOCS_H_INCLUDED
#define FOCS_H_INCLUDED

#define FALSE 0
#define TRUE 1

#define DefCell(EltType,CellType,ListType)  \
typedef struct tag##CellType* ListType;          \
typedef struct tag##CellType {                           \
  EltType element;                           \
  ListType next;                            \
} CellType;                                          \


DefCell(int,IntCell,IntList)
DefCell(float,FloatCell,FloatList)
DefCell(char,CharCell,CharList)

#endif // FOCS_H_INCLUDED
