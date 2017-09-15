

struct Node(i32);
struct Arc(Node,Node);


fn main()
{
    let arc = Arc(Node(1),Node(2));
}