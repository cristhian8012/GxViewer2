namespace gxViewer.GxDebugger
{
    public enum InstructionType
    {
        Asignacion,
        AsignacionDesdeAtributo,
        If, EndIf,
        ForEach, EndFor, Exit,
        DoCase, Case, Otherwise, EndCase,
        Do, EndDo,
        Call, Return,
        Comentario, Raw, Desconocido
    }
}
