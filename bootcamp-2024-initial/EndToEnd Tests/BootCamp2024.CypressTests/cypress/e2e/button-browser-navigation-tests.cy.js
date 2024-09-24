describe('Button And Browser Navigation Test', () => {
    beforeEach(() => {
      cy.visit('http://localhost:5174');
    });
  
    it('Should navigate back correctly using the browser back button', () => {
      cy.get('.btn-get-started').click();
      cy.url().should('eq', 'http://localhost:5174/authors');
  
      cy.get('#home-button').click();
      cy.url().should('eq', 'http://localhost:5174/');
  
      cy.get('#authors-button').click();
      cy.url().should('eq', 'http://localhost:5174/authors');
  
      cy.get('.card').first().find('.view-btn').click();
      cy.url().should('eq', 'http://localhost:5174/authors/1');
  
      cy.go('back');
      cy.url().should('eq', 'http://localhost:5174/authors');

      cy.go('back');
      cy.url().should('eq', 'http://localhost:5174/');
    });
  });
  