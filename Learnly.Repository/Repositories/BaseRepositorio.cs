public abstract class BaseRepositorio
{
    protected readonly LearnlyContexto _contexto;

    protected BaseRepositorio(LearnlyContexto contexto)
    {
        _contexto = contexto;
    }
}
