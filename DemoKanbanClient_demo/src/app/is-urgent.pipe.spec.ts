import { IsUrgentPipe } from './is-urgent.pipe';

describe('IsUrgentPipe', () => {
  it('create an instance', () => {
    const pipe = new IsUrgentPipe();
    expect(pipe).toBeTruthy();
  });
});
