export default interface ReceiptItem {
    dateCreated: string;
    dateModified: string;
    ean: string;
    id: number;
    isRemoved: boolean;
    name: string;
    ordinalNumber: number;
    price: string;
    productId: number;
    quantity: number;
    receiptId: number;
    taxRate: number;
    taxValue: number;
    unit: string;
    value: string;
}