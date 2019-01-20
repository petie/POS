import Shift from "../Models/Shift";
import Product from "../Models/Product";
import ReceiptItem from "../Models/ReceiptItem";

export interface AppState {
    receiptItems?: ReceiptItem[];
    products?: Product[];
    shift?: Shift;
    selectedReceiptItem: number;
    showShiftDialog: boolean;
    openShift: boolean;
    closeShift: boolean;
    showProductDialog: boolean;
    showPayDialog: boolean;
    receiptTotal?: string;
    showSnackBar: boolean;
    snackBarType?: "success" | "warning" | "error"
}
