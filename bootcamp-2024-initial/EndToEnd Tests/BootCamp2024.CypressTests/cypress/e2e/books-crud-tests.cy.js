describe('Books - CRUD Operations tests', () => {
  beforeEach(() => {
    cy.visit('http://localhost:5174/authors/1');
  });

  it('Should view books', () => {  
    cy.get('.card-group-books').should('exist');
    cy.get('.card-group-books .book-card').should('have.length', 3);
  
    cy.get('.card-group-books .book-card').eq(0).within(() => {
        cy.contains('Romeo and Juliet').should('exist');
        cy.contains('Year published: 1597').should('exist');
      });
      
      cy.get('.card-group-books .book-card').eq(1).within(() => {
        cy.contains('Hamlet').should('exist');
        cy.contains('Year published: 1600').should('exist');
      });
      
      cy.get('.card-group-books .book-card').eq(2).within(() => {
        cy.contains('Othello').should('exist');
        cy.contains('Year published: 1603').should('exist');
      });
  });

  it('Should add a book', () => {  
    cy.get('.add-btn').click();
  
    cy.getInputByName('title').type('Macbeth');
    cy.getInputByName('yearPublished').clear().type('1600');
  
    cy.get('.form-submit').click();
  
    cy.get('.card-group-books .book-card').should('have.length', 4);

    cy.get('.card-group-books .book-card').last().within(() => {
      cy.contains('Macbeth').should('exist');
      cy.contains('Year published: 1600').should('exist');
    });
  });
  
  it('Should edit a book', () => {
    cy.get('.card-group-books .book-card').last().within(() => {
      cy.get('.edit-btn').click();
    });
  
    cy.getInputByName('title').clear().type('Julius Caesar');
    cy.getInputByName('yearPublished').clear().type('1599');
  
    cy.get('.form-submit').click();

    cy.get('.card-group-books .book-card').should('have.length', 4);
  
    cy.get('.card-group-books .book-card').last().within(() => {
      cy.contains('Julius Caesar').should('exist');
      cy.contains('Year published: 1599').should('exist');
    });
  });

  it('Should delete a book', () => {  
    cy.get('.card-group-books .book-card').last().within(() => {
      cy.get('.delete-btn').click();
    });

    cy.get('.card-group-books .book-card').should('have.length', 3);
  
    cy.get('.card-group-books .book-card').last().within(() => {
      cy.contains('Julius Caesar').should('not.exist');
      cy.contains('Year published: 1599').should('not.exist');
    });
  });  
});