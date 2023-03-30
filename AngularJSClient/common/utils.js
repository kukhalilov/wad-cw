function getPaginationPages(currentPage, totalPages) {
  var pages = [];
  var startPage, endPage;

  if (totalPages <= 10) {
    startPage = 1;
    endPage = totalPages;
  } else {
    if (currentPage <= 6) {
      startPage = 1;
      endPage = 10;
    } else if (currentPage + 4 >= totalPages) {
      startPage = totalPages - 9;
      endPage = totalPages;
    } else {
      startPage = currentPage - 5;
      endPage = currentPage + 4;
    }
  }

  for (var i = startPage; i <= endPage; i++) {
    pages.push(i);
  }

  return pages;
}
