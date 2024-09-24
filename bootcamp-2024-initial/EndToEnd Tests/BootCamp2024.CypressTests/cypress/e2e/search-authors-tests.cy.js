describe('Authors - Search feature tests', () => {
    beforeEach(() => {
      cy.visit('http://localhost:5174/authors');
    });
  
    it('Should search for authors based on their name', () => {
        cy.get('#search').type('William');
    
        cy.get('.card').should('have.length', 1); 
        cy.get('.card').within(() => {
          cy.contains('William Shakespeare').should('exist');
        });
    
        cy.get('#search').clear();
        cy.get('.card').should('have.length', 4);
    });
    
    it('Should search for authors based on their surname', () => {
    cy.get('#search').type('Shakespeare');

    cy.get('.card').should('have.length', 1); 
    cy.get('.card').within(() => {
        cy.contains('William Shakespeare').should('exist');
    });

    cy.get('#search').clear();
    cy.get('.card').should('have.length', 4);
    });

    it('Should search for authors based on a random string their name or surname contains', () => {
        cy.get('#search').type('C');
    
        cy.get('.card').should('have.length', 2); 
        cy.get('.card').contains('Agatha Christie').should('exist');
        cy.get('.card').contains('Barbara Cartland').should('exist');
    
        cy.get('#search').clear();
        cy.get('.card').should('have.length', 4);
    });
  });