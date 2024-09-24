  Cypress.Commands.add('getInputByName', (name) => {
    return cy.get(`input[name="${name}"]`);
  });