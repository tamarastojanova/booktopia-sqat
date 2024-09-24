describe('Basic Homepage tests', () => {
  beforeEach(() => {
    cy.visit('http://localhost:5174');
  });

  it('Should have the correct homepage title', () => {
    cy.title().should('eq', 'Booktopia');
  });

  it('Should have a cover image', () => {
    cy.get('.cover').should('exist');
    cy.get('.cover').should('have.css', 'background-image').and('include', 'cover-image.jpg');
  });

  it('Should have three buttons - Home, Authors and Get Started', () => {
    cy.get('#home-button').should('exist');
    cy.get('#authors-button').should('exist');
    cy.get('#get-started-button').should('exist');
  });

  it('Should navigate to the correct URL when clicking Home button', () => {
    cy.get('#home-button').contains('Home').click();
    cy.url().should('eq', 'http://localhost:5174/');
  });

  it('Should navigate to the correct URL when clicking Authors button', () => {
    cy.get('#authors-button').click();
    cy.url().should('eq', 'http://localhost:5174/authors');
  });

  it('Should navigate to the correct URL when clicking Get started button', () => {
    cy.get('#get-started-button').click();
    cy.url().should('eq', 'http://localhost:5174/authors');
  });
});