import Product from "../Models/Product";
import axios from "axios";

const initialState = {
    products: [] as Product[],
    showProductDialog: false,
    isLoading: false
}

export const ACTION_TYPES = {
    FETCH_PRODUCTS: 'products/FETCH_PRODUCTS'
}

export type ProductState = Readonly<typeof initialState>;

export default (state: ProductState = initialState, action: any): ProductState => {
    return state;
}

export const showProductDialog = () => async (dispatch: any, getState: any) => {
    return {
        type: ACTION_TYPES.FETCH_PRODUCTS,
        payload: axios.get("api/products")
    };
}