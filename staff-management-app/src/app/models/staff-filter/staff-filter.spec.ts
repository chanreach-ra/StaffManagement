import { StaffFilter } from './staff-filter';

describe('StaffFilter', () => {
  it('should create an instance', () => {
    expect(new StaffFilter('000000002',0,null,null)).toBeTruthy();
  });
});
