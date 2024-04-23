import { Staff } from './staff';

describe('Staff', () => {
  it('should create an instance', () => {
    expect(new Staff(0, '00000009', 'Somnang Forever', '2000-04-04', '', 0, new Date)).toBeTruthy();
  });
});
