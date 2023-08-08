import { Book } from "./Book";
import { BaseApiResponse } from "./BaseApi";

// TODO: Remove this, make get request in API.
// export class GoogleBooksSearchRequest {
//   title!: string;
// }

export class GoogleBooksSearchResponse extends BaseApiResponse {
  books: Book[] = []; 
}
