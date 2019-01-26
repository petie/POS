import Product from "../Models/Product";

const initialState = {
    products: [] as Product[],
    showProductDialog: false,
    isLoading: false
}

export type ProductState = Readonly<typeof initialState>;

export default (state: ProductState = initialState, action: any): ProductState => {
    return state;
}