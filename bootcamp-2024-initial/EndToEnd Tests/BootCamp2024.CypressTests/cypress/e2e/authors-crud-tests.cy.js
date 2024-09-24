describe('Authors - CRUD Operations', () => {
    const baseAuthorsUrl = 'http://localhost:5174/authors';

    it('Should view authors', () => {
      cy.visit(baseAuthorsUrl);
  
      cy.get('.card').should('have.length', 4);
  
      cy.get('.card').each(($card) => {
          cy.wrap($card).find('.author-name').should('exist');
          cy.wrap($card).find('.author-img').should('have.attr', 'src').and('not.be.empty');
      });
    });  
  
    it('Should create an author', () => {
      cy.visit(baseAuthorsUrl + '/form');

      cy.getInputByName('firstName').type('Anne');
      cy.getInputByName('lastName').type('Frank');
      cy.getInputByName('imageUrl').type('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF');
      
      cy.get('.form-submit').click();

      cy.get('.card').last().should('exist');
      cy.get('.card').last().contains('Anne').should('exist');
      cy.get('.card').last().contains('Frank').should('exist');
      cy.get('.card img').last()
        .should('have.attr', 'src')
        .and('eq', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF');
    });

    it('Should edit an author', () => {
        cy.visit(baseAuthorsUrl);
  
        cy.get('.card').last().find('.edit-btn').click();
        
        cy.getInputByName('firstName').clear().type('George'); 
        cy.getInputByName('lastName').clear().type('Orwell');
        cy.getInputByName('imageUrl').clear().type('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8cwItnEGlr95H3OsMQJgJQBjrb-DKRLTrLQ&s');
        
        cy.get('.form-submit').click();
        
        cy.get('.card').last().should('exist');
        cy.get('.card').last().contains('George').should('exist');
        cy.get('.card').last().contains('Orwell').should('exist');
        cy.get('.card img').last()
            .should('have.attr', 'src')
            .and('eq', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8cwItnEGlr95H3OsMQJgJQBjrb-DKRLTrLQ&s');
      });
      
      it('Should delete an author', () => {
        cy.visit(baseAuthorsUrl);
      
        cy.get('.card').last().find('.delete-btn').click();
      
        cy.get('.card').last().contains('George').should('not.exist');
        cy.get('.card').last().contains('Orwell').should('not.exist');
      });

      it('Should view an author', () => {
        cy.visit(baseAuthorsUrl);
      
        cy.get('.card').first().find('.view-btn').click();
      
        cy.get('.card').contains('William').should('exist');
        cy.get('.card').contains('Shakespeare').should('exist');
              cy.get('.author-img')
          .should('have.attr', 'src')
          .and('eq', 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg');
      
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
  });
  