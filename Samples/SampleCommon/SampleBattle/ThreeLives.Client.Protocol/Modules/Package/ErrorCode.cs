namespace TLClient.Protocol.Modules.Package
{
    public enum ErrorCode
    {
        None = 0,
        PackageNotExist,
        OutOfStack,
        OutOfBagSize,
        ExistItem,
        NotExistItem,
        EmptySlotsNotEnough,
        NotEnoughItem,
        ItemCountNotEnough,
        ArgumentError,
        Unknown,
        Max,
    }
}