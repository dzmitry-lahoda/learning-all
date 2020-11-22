
public interface IA
{
    void Do();
}

public interface I<in T> where T:IA
{
	void Do(T x);
}

public class C<T, TB>: IA
 where T : I<TB>
 where TB: IA
 
{
    public void Do() {
    	T x = default;
        x.Do(this);
    }
}