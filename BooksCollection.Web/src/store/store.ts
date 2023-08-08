import { configureStore } from "@reduxjs/toolkit";
import booksSlice from "./booksSlice";
import { useDispatch } from "react-redux";

const reducer = {
  books: booksSlice,
};

export const store = configureStore({
  reducer,
});

export const useAppDispatch = () => useDispatch<typeof store.dispatch>();
