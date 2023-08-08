import { Book } from "./Book";

export type BookSliceContext = {
  books: Book[];
  addModifyBook: Book,
  googleSearchBooks: Book[],
};
