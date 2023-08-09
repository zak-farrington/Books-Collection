import React, { useEffect } from "react";
import { useSelector } from "react-redux";
import { getBooksList, selectBooks } from "../../store/booksSlice";
import BookshelfRow from "../BookshelfRow/BookshelfRow";
import BookshelfActions from "../BookshelfActions/BookshelfActions";
import "./Bookshelf.less";
import { useAppDispatch } from "../../store/store";

const Bookshelf = () => {
    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(getBooksList);
    }, []);

    const books = useSelector(selectBooks);

    const totalBooksPerPage = 15;
    const booksPerRow = 5;

    const pages = [];

    for (let x = 0; x < books.length; x += totalBooksPerPage) {
        const newPage = [];
        const booksForPage = books.slice(x, x+ totalBooksPerPage);
        for (let y = 0; y < booksForPage.length; y += booksPerRow) {
            const row = booksForPage.slice(y, y + booksPerRow);
            newPage.push(row);
        }
        pages.push(newPage);
    }

    return (
        <div className="bookshelf">
            <div className="bookshelfActionsContainer">
                <BookshelfActions/>
            </div>

            <div id="bookCarousel" className="carousel slide" data-ride="carousel">
                <div className="carousel-inner">
                    {pages.map((page, pageIndex) => (
                        <div key={`page-${pageIndex}`} className={`carousel-item ${pageIndex === 0 ? 'active' : ''}`}>
                            {page.map((row, rowIndex) => (
                                <div key={`row-${pageIndex}-${rowIndex}`} className="bookshelfRowContainer">
                                    <BookshelfRow booksForRow={row} />
                                </div>
                            ))}
                        </div>
                    ))}
                </div>

                <a className="carousel-control-prev" href="#bookCarousel" role="button" data-slide="prev">
                    <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span className="sr-only">Previous</span>
                </a>
                <a className="carousel-control-next" href="#bookCarousel" role="button" data-slide="next">
                    <span className="carousel-control-next-icon" aria-hidden="true"></span>
                    <span className="sr-only">Next</span>
                </a>
            </div>
        </div>
    );
}

export default Bookshelf;
