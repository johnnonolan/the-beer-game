using System;

namespace Boozy
{
    public interface IBrewery
    {
        GameStatusViewModel CreateGame();
        GameStatusViewModel EndTurn(Guid gameId, int order, int retailOrder, int wholeSaleOrder, int distributorOrder);
    }
}