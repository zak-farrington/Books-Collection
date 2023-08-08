import { BaseApiResponse } from "./BaseApi";
import { Book } from "./Book";

export class BooksListRequest {}

export class BooksListResponse extends BaseApiResponse {
  books: Book[] = [];
}
