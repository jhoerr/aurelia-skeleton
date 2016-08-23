import {App} from '../../src/app';

class RouterStub {
  configure(handler) {
    handler(this);
  }

  map(routes) {
    this.routes = routes;
  }
}

describe('the App module', () => {
  var sut;
  var mockedRouter;

  beforeEach(() => {
    mockedRouter = new RouterStub();
    sut = new App(mockedRouter, {}, {});
    sut.configure(mockedRouter, mockedRouter);
  });

  it('contains a router property', () => {
    expect(sut.router).toBeDefined();
  });

  it('configures the router title', () => {
      expect(sut.router.title).toEqual('Example Aurelia App');
  });

  it('should have a welcome route', () => {
    expect(sut.router.routes).toContain({ route: ['', 'welcome'], name: 'welcome',  moduleId: 'welcome', nav: true, title: 'Welcome' });
  });
});
