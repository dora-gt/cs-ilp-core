using System;

namespace Org.Interledger.Core.InterledgerPacket
{
    public enum PacketTypes : byte
    {
        // 1-7, 9-11 is used for ILPv1
        // 8, 12-14 is used for ILPv4
        /*
        ILP_PAYMENT_TYPE = 1,
        ILQP_QUOTE_LIQUIDITY_REQUEST_TYPE = 2,
        ILQP_QUOTE_LIQUIDITY_RESPONSE_TYPE = 3,
        ILQP_QUOTE_BY_SOURCE_AMOUNT_REQUEST_TYPE = 4,
        ILQP_QUOTE_BY_SOURCE_AMOUNT_RESPONSE_TYPE = 5,
        ILQP_QUOTE_BY_DESTINATION_AMOUNT_REQUEST_TYPE = 6,
        ILQP_QUOTE_BY_DESTINATION_AMOUNT_RESPONSE_TYPE = 7,
        */
        INTERLEDGER_PROTOCOL_ERROR = 8,
        /*
        ILP_FULFILLMENT_TYPE = 9,
        ILP_FORWARDED_PAYMENT_TYPE = 10,
        ILP_REJECTION_TYPE = 11,
        */
        ILP_PAREPARE_TYPE = 12,
        ILP_FULFILL_TYPE = 13,
        ILP_REJECT_TYPE = 14,
    }
}
