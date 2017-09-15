struct Node
{
  value:i32
}

fn build_node(value:i32) -> Node
{
    Node { value }
}


fn o(value:i32) -> Node
{
    Node { value }
}


struct Arc
{
    from:Node,
    to:Node 
}

fn build_arc(from:Node,to:Node) -> Arc
{
   Arc {from, to}
}

fn u(from:Node,to:Node) -> Arc
{
   Arc {from, to}
}




fn main()
{    
    let a = [u(o(0),o(0)),u(o(0),o(1)), u(o(0),o(2)), u(o(1),o(3)), u(o(2),o(0)), u(o(2),o(1)), u(o(2),o(4)),
             u(o(3),o(2)), u(o(3),o(4)), u(o(4),o(1))];
             
    println!("Graph algorithms");
}