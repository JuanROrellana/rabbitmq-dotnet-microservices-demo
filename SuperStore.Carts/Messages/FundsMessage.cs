using SuperStore.Shared.Connections;

namespace SuperStore.Carts.Messages;

public record FundsMessage(long CustomerId, decimal CurrentFunds) :  IMessage;