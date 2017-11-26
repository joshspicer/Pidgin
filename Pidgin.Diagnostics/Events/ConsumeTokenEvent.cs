namespace Pidgin.Diagnostics.Events
{
    public sealed class ConsumeTokenEvent<TToken>
    {
        public TToken ConsumedToken { get; }
        public int OldPos { get; }
        public int NewPos { get; }
        public SourcePos OldSourcePos { get; }
        public SourcePos NewSourcePos { get; }

        internal ConsumeTokenEvent(TToken consumedToken, int oldPos, int newPos, SourcePos oldSourcePos, SourcePos newSourcePos)
        {
            ConsumedToken = consumedToken;
            OldPos = oldPos;
            NewPos = newPos;
            OldSourcePos = oldSourcePos;
            NewSourcePos = newSourcePos;
        }
    }
}