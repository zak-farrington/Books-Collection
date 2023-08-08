
export enum BookCategory {
    Technology,
    Coding,
    Spirituality,
    Meditation,
    Music,
    SelfHelp,
    Other
  }
  
  export enum MsrpUnit {
    Usd,
  }
  
  export class Book {
    id?: number;
    uid?: string | null;
    creationDate?: Date;
    lastUpdatedDate?: Date;
    title?: string; 
    description?: string | null;
    authorName?: string;
    publishedDate?: Date | null;
    msrp?: number | null;
    msrpUnit?: MsrpUnit | null;
    isbn?: string | null;
    category?: BookCategory | null;
    otherCategoryName?: string | null;
    imageUrl?: string | null;
}

  
  